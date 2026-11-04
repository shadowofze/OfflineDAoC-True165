using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace DOL.GS.SmartAI
{
    public enum SmartClaimKind : byte { CrowdControl, Heal, Interrupt }

    /// <summary>
    /// A group's SmartAI blackboard (plan Phase 7, architecture section 4.2). One per group, weakly held: a disbanded
    /// group takes it with it.
    /// - Current: the snapshot all members read, replaced whole by the coordinator (lock-free reads).
    /// - Claims: "I am mezzing that add" / "I am healing that member", so two bots don't spend on the same thing.
    ///   Claimed from worker threads under a per-group lock (never across groups); expired ones are pruned each tick.
    /// </summary>
    public sealed class SmartGroupTacticalState
    {
        private static readonly ConditionalWeakTable<Group, SmartGroupTacticalState> States = new();

        /// <summary>A group whose SmartAI bots haven't thought in combat for this long stops publishing.</summary>
        public const long ActiveForMs = 5_000;

        private SmartGroupTacticalSnapshot _current = SmartGroupTacticalSnapshot.Empty;
        private long _lastTouched;
        private readonly Lock _claimLock = new();
        private readonly Dictionary<(SmartClaimKind Kind, object Target), Claim> _claims = new();

        private readonly record struct Claim(object Owner, long ExpiresAt);

        /// <summary>Observations kept between pre-passes (architecture section 4.3); the oldest goes when full.</summary>
        public const int ObservationCapacity = 32;
        /// <summary>A bot's observations per tick.</summary>
        public const int MaxObservationsPerBotPerTick = 2;

        private readonly Lock _observationLock = new();
        private readonly Queue<SmartBotObservation> _observations = new(ObservationCapacity);

        /// <summary>The callout throttle (pre-pass only).</summary>
        internal SmartGroupCalloutPolicy Callouts { get; } = new();

        /// <summary>Posts an observation (worker threads). False when the bot already posted its share this tick.</summary>
        public bool Post(in SmartBotObservation observation)
        {
            lock (_observationLock)
            {
                int mine = 0;
                foreach (SmartBotObservation o in _observations)
                    if (o.PosterId == observation.PosterId && o.Tick == observation.Tick)
                        mine++;
                if (mine >= MaxObservationsPerBotPerTick)
                    return false;
                if (_observations.Count >= ObservationCapacity)
                    _observations.Dequeue();
                _observations.Enqueue(observation);
                return true;
            }
        }

        /// <summary>Takes every posted observation (the pre-pass).</summary>
        public List<SmartBotObservation> DrainObservations()
        {
            lock (_observationLock)
            {
                var all = new List<SmartBotObservation>(_observations);
                _observations.Clear();
                return all;
            }
        }

        public static SmartGroupTacticalState For(Group group) => States.GetValue(group, static _ => new SmartGroupTacticalState());

        public static bool TryGet(Group group, out SmartGroupTacticalState state)
        {
            state = null;
            return group != null && States.TryGetValue(group, out state);
        }

        /// <summary>Every group with a blackboard (the coordinator's pre-pass).</summary>
        internal static IEnumerable<KeyValuePair<Group, SmartGroupTacticalState>> All => States;

        public SmartGroupTacticalSnapshot Current => Volatile.Read(ref _current);

        public void Publish(SmartGroupTacticalSnapshot snapshot) => Volatile.Write(ref _current, snapshot);

        /// <summary>A SmartAI bot of the group thought in combat: keep publishing.</summary>
        public void Touch(long now) => Interlocked.Exchange(ref _lastTouched, now);

        public bool IsActive(long now) => now - Interlocked.Read(ref _lastTouched) <= ActiveForMs;

        /// <summary>Claims a target for this owner until expiresAt. False when another owner holds a live claim.</summary>
        public bool TryClaim(SmartClaimKind kind, object target, object owner, long now, long expiresAt)
        {
            if (target == null || owner == null)
                return false;
            lock (_claimLock)
            {
                if (_claims.TryGetValue((kind, target), out Claim claim) && claim.ExpiresAt > now && !ReferenceEquals(claim.Owner, owner))
                    return false;
                _claims[(kind, target)] = new Claim(owner, expiresAt);
                return true;
            }
        }

        /// <summary>Another owner holds a live claim on the target.</summary>
        public bool IsClaimedByOther(SmartClaimKind kind, object target, object owner, long now)
        {
            if (target == null)
                return false;
            lock (_claimLock)
                return _claims.TryGetValue((kind, target), out Claim claim) && claim.ExpiresAt > now && !ReferenceEquals(claim.Owner, owner);
        }

        public void Release(SmartClaimKind kind, object target, object owner)
        {
            lock (_claimLock)
            {
                if (_claims.TryGetValue((kind, target), out Claim claim) && ReferenceEquals(claim.Owner, owner))
                    _claims.Remove((kind, target));
            }
        }

        public int ClaimCount(long now)
        {
            lock (_claimLock)
            {
                int n = 0;
                foreach (Claim c in _claims.Values)
                    if (c.ExpiresAt > now)
                        n++;
                return n;
            }
        }

        /// <summary>Drops expired claims (the coordinator, once per tick).</summary>
        public void PruneClaims(long now)
        {
            lock (_claimLock)
            {
                if (_claims.Count == 0)
                    return;
                List<(SmartClaimKind, object)> expired = null;
                foreach (var (key, claim) in _claims)
                    if (claim.ExpiresAt <= now)
                        (expired ??= []).Add(key);
                if (expired != null)
                    foreach (var key in expired)
                        _claims.Remove(key);
            }
        }
    }
}
