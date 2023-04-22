using System.Collections.Generic;
using ProgesorCreating.Stats;
using UnityEngine;

namespace ProgesorCreating.Inventories
{
    [CreateAssetMenu(menuName = ("Inventory/New Equipable Item"))]
    public class StatsEquipableItem : EquipableItem,IModifierProvider
    {
        [SerializeField] private Modifier[] additiveModifiers;
        [SerializeField] private Modifier[] percentageModifiers;
        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            foreach (var modifier in additiveModifiers)
            {
                if (modifier.stat==stat)
                {
                    yield return modifier.value;
                }
            }
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            foreach (var modifier in percentageModifiers)
            {
                if (modifier.stat==stat)
                {
                    yield return modifier.value;
                }
            }
        }
    }
}