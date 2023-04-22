using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Inventories
{
    [CreateAssetMenu(menuName = ("Inventory/New Drop Library"))]
    public class DropLibrary : ScriptableObject
    {
        [SerializeField] DropConfig[] potentialDrops;
        [SerializeField] float[] dropChangePercentage;
        [SerializeField] int[] minDrops;
        [SerializeField] int[] maxDrops;

        public IEnumerable<Dropped> GetRandomDrops(int level)
        {
            if (!ShouldRandomDrop(level))
            {
                yield break;
            }

            for (int i = 0; i < GetRandomNumberOfDrops(level); i++)
            {
                yield return GetRandomDrop(level);
            }
        }

        private bool ShouldRandomDrop(int level)
        {
            return Random.Range(0, 100) < GetByLevel(dropChangePercentage, level);
        }

        private int GetRandomNumberOfDrops(int level)
        {
            int min = GetByLevel(minDrops, level);
            int max = GetByLevel(maxDrops, level);
            return Random.Range(min, max);
        }

        private Dropped GetRandomDrop(int level)
        {
            var drop = SelectRandomItem(level);
            var result = new Dropped();
            result.Item = drop.Item;
            result.Number = drop.GetRandomNumber(level);
            return result;
        }

        private DropConfig SelectRandomItem(int level)
        {
            float totalChance = GetTotalChance(level);
            float randomRoll = Random.Range(0, totalChance);
            float changeTotal = 0;
            foreach (var drop in potentialDrops)
            {
                changeTotal += GetByLevel(drop.RelativeChance,level);
                if (changeTotal>randomRoll)
                {
                    return drop;
                }
            }

            return null;
        }

        private float GetTotalChance(int level)
        {
            float total = 0;
            foreach (var drop in potentialDrops)
            {
                total += GetByLevel(drop.RelativeChance,level);
            }

            return total;
        }

        static T GetByLevel<T>(T[] values, int level)
        {
            if (values.Length==0)
            {
                return default;
            }

            if (level>values.Length)
            {
                return values[values.Length - 1];
            }

            if (level<=0)
            {
                return default;
            }

            return values[level - 1];
        }
    }
}