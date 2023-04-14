using System;
using ProgesorCreating.RPG.Combat;
using ProgesorCreating.RPG.Core;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        private Fighter _fighter;
        private Health _health;
        private GameObject _player;
        

        private void Start()
        {
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
            _player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            if (_health.IsDead())return;
            
            if (InAttackRangeOfPlayer() && _fighter.CanAttack(_player))
            {
                _fighter.Attack(_player);
            }
            else
            {
                _fighter.Cancel();
            }
        }

        private bool InAttackRangeOfPlayer()
        {
            float distance = Vector3.Distance(_player.transform.position, transform.position);
            return distance<chaseDistance;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color=Color.red;
            Gizmos.DrawWireSphere(transform.position,chaseDistance);
        }
    }
}