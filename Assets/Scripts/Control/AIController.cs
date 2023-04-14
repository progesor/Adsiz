using System;
using ProgesorCreating.RPG.Combat;
using ProgesorCreating.RPG.Core;
using UnityEditor;
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
        
        [SerializeField] private bool debug = false;
        [SerializeField] Color chaseColor = new Color(1, 1, 0, 0.1f);

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