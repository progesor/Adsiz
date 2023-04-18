﻿using ProgesorCreating.RPG.Core;
using ProgesorCreating.RPG.Saving;
using ProgesorCreating.RPG.Stats;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Attributes
{
    public class Health : MonoBehaviour,ISaveable
    {
        [SerializeField] private float healthPoints = 100f;

        private bool _isDead;
        private BaseStats _baseStats;
        private static readonly int Die1 = Animator.StringToHash("die");

        private void Awake()
        {
            _baseStats = GetComponent<BaseStats>();
            healthPoints = _baseStats.GetHealth();
        }

        public bool IsDead()
        {
            return _isDead;
        }
        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints==0)
            {
               Die();
            }
        }

        public float GetPercentage()
        {
            return 100 * (healthPoints / _baseStats.GetHealth());
        }

        private void Die()
        {
            if (_isDead)return;

            _isDead = true;
            GetComponent<Animator>().SetTrigger(Die1);
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;

            if (healthPoints <= 0)
            {
                Die();
            }
        }
    }
}