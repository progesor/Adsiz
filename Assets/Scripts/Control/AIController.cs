using ProgesorCreating.RPG.Attributes;
using ProgesorCreating.RPG.Combat;
using ProgesorCreating.RPG.Core;
using ProgesorCreating.RPG.Movement;
using ProgesorCreating.RPG.Utils;
using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float suspicionTime = 3f;
        [SerializeField] private float aggroCooldownTime = 5f;
        [SerializeField] private Waypoints patrolPath;
        [SerializeField] private float waypointTolerance = 1f;
        [SerializeField] private float waypointDwellTime = 3f;
        [Range(0,1)]
        [SerializeField] private float patrolSpeedFraction = 0.4f;
        [SerializeField] private float shoutDistance = 5f;
        
        private Fighter _fighter;
        private Health _health;
        private Mover _mover;
        private GameObject _player;

        private LazyValue<Vector3> _guardPosition;
        private float _timeSinceLastSawPlayer = Mathf.Infinity;
        private float _timeSinceArriveAtWaypoint = Mathf.Infinity;
        private float _timeSinceAggravated = Mathf.Infinity;
        private int _currentWaypointIndex;
        
        [SerializeField] private bool debug;
        [SerializeField] Color chaseColor = new Color(1, 1, 0, 0.1f);
        [SerializeField] Color weaponRangeColor = new Color(1, 0, 0, 0.2f);

        private void Awake()
        {
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
            _mover = GetComponent<Mover>();
            _player = GameObject.FindWithTag("Player");
            _guardPosition = new LazyValue<Vector3>(GetGuardPosition);
        }

        private Vector3 GetGuardPosition()
        {
            return transform.position;
        }

        private void Start()
        {
            _guardPosition.ForceInit();
        }

        private void Update()
        {
            if (_health.IsDead())return;
            
            if (IsAggravated() && _fighter.CanAttack(_player))
            {
                AttackBehaviour();
            }
            else if (_timeSinceLastSawPlayer<suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }

            UpdateTimers();
        }

        public void Aggravate()
        {
            _timeSinceAggravated = 0;
        }

        private void UpdateTimers()
        {
            _timeSinceLastSawPlayer += Time.deltaTime;
            _timeSinceArriveAtWaypoint += Time.deltaTime;
            _timeSinceAggravated += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = _guardPosition.value;

            if (patrolPath!=null)
            {
                if (AtWaypoint())
                {
                    _timeSinceArriveAtWaypoint = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }

            if (_timeSinceArriveAtWaypoint>waypointDwellTime)
            {
                _mover.StartMovementAction(nextPosition, patrolSpeedFraction);
            }
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void CycleWaypoint()
        {
            _currentWaypointIndex = patrolPath.GetNextIndex(_currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.points[_currentWaypointIndex];
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            _timeSinceLastSawPlayer = 0;
            _fighter.Attack(_player);

            AggravateNearbyEnemies();
        }

        private void AggravateNearbyEnemies()
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, shoutDistance, Vector3.up, 0);

            foreach (RaycastHit hit in hits)
            {
                AIController ai = hit.collider.GetComponent<AIController>();
                if (ai ==null)continue;
                
                ai.Aggravate();
            }
        }

        private bool IsAggravated()
        {
            float distance = Vector3.Distance(_player.transform.position, transform.position);
            return distance < chaseDistance || _timeSinceAggravated < aggroCooldownTime;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (debug)
            {
                Handles.color = chaseColor;
                Handles.DrawSolidDisc(transform.position, Vector3.up, chaseDistance);
                Handles.color = weaponRangeColor;
                Handles.DrawSolidDisc(transform.position, Vector3.up, GetComponent<Fighter>().GetWeaponRange());
            }
        }
#endif
    }
}