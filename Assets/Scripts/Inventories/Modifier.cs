using System;
using ProgesorCreating.Stats;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Inventories
{
    [Serializable]
    public struct Modifier
    {
        public Stat stat;
        public float value;
    }
}