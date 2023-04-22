using System;
using Random = UnityEngine.Random;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Inventories
{
    [Serializable]
    public class DropConfig
    {
        public InventoryItem Item;
        public float[] RelativeChance;
        public int[] MinNumber;
        public int[] MaxNumber;

        public int GetRandomNumber(int level)
        {
            if (!Item.IsStackable())
            {
                return 1;
            }

            int min = GetByLevel(MinNumber, level);
            int max = GetByLevel(MaxNumber, level);
            return Random.Range(min, max + 1);
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