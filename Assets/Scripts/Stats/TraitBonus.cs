using System;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Stats
{
    [Serializable]
    public class TraitBonus
    {
        public Trait trait;
        public Stat stat;
        public float additiveBonusPerPoint;
        public float percentageBonusPerPoint;
    }
}