using System.Collections.Generic;
using ProgesorCreating.Inventories;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Abilities
{
    public class CooldownStore : MonoBehaviour
    {
        private Dictionary<InventoryItem, float> _cooldownTimers = new Dictionary<InventoryItem, float>();
        private Dictionary<InventoryItem, float> _initialCooldownTimers = new Dictionary<InventoryItem, float>();

        private void Update()
        {
            List<InventoryItem> keys = new List<InventoryItem>(_cooldownTimers.Keys);
            foreach (InventoryItem ability in keys)
            {
                _cooldownTimers[ability] -= Time.deltaTime;
                if (_cooldownTimers[ability] < 0)
                {
                    _cooldownTimers.Remove(ability);
                    _initialCooldownTimers.Remove(ability);
                }
            }
        }

        public void StartCooldown(InventoryItem ability, float cooldownTime)
        {
            _cooldownTimers[ability] = cooldownTime;
            _initialCooldownTimers[ability] = cooldownTime;
        }

        public float GetTimeRemaining(InventoryItem ability)
        {
            if (!_cooldownTimers.ContainsKey(ability))
            {
                return 0;
            }

            return _cooldownTimers[ability];
        }

        public float GetFractionRemaining(InventoryItem ability)
        {
            if (ability==null)
            {
                return 0;
            }
            if (!_initialCooldownTimers.ContainsKey(ability))
            {
                return 0;
            }

            return _cooldownTimers[ability] / _initialCooldownTimers[ability];
        }
    }
}