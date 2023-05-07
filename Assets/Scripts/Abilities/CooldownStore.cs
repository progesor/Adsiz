using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Abilities
{
    public class CooldownStore : MonoBehaviour
    {
        private Dictionary<Ability, float> _cooldownTimers = new Dictionary<Ability, float>();

        private void Update()
        {
            List<Ability> keys = new List<Ability>(_cooldownTimers.Keys);
            foreach (Ability ability in keys)
            {
                _cooldownTimers[ability] -= Time.deltaTime;
                if (_cooldownTimers[ability] < 0)
                {
                    _cooldownTimers.Remove(ability);
                }
            }
        }

        public void StartCooldown(Ability ability, float cooldownTime)
        {
            _cooldownTimers[ability] = cooldownTime;
        }

        public float GetTimeRemaining(Ability ability)
        {
            if (!_cooldownTimers.ContainsKey(ability))
            {
                return 0;
            }

            return _cooldownTimers[ability];
        }
    }
}