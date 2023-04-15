using ProgesorCreating.RPG.Combat;
using ProgesorCreating.RPG.Core;
using ProgesorCreating.RPG.Movement;
using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float suspicionTime = 3f;
        private Fighter _fighter;
        private Health _health;
        private Mover _mover;
        private GameObject _player;

        private Vector3 _guardPosition;
        private float _timeSinceLastSawPlayer = Mathf.Infinity;
        
        [SerializeField] private bool debug;
        [SerializeField] Color chaseColor = new Color(1, 1, 0, 0.1f);

        private void Start()
        {
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
            _mover = GetComponent<Mover>();
            _player = GameObject.FindWithTag("Player");

            _guardPosition = transform.position;
        }

        private void Update()
        {
            if (_health.IsDead())return;
            
            if (InAttackRangeOfPlayer() && _fighter.CanAttack(_player))
            {
                _timeSinceLastSawPlayer = 0;
                AttackBehaviour();
            }
            else if (_timeSinceLastSawPlayer<suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                GuardBehaviour();
            }

            _timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void GuardBehaviour()
        {
            _mover.StartMovementAction(_guardPosition);
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            _fighter.Attack(_player);
        }

        private bool InAttackRangeOfPlayer()
        {
            float distance = Vector3.Distance(_player.transform.position, transform.position);
            return distance<chaseDistance;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (debug)
            {
                Handles.color = chaseColor;
                Handles.DrawSolidDisc(transform.position, Vector3.up, chaseDistance);
            }
        }
#endif
    }
}