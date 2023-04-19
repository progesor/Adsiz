using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Stats
{
    public interface IModifierProvider
    {
        IEnumerable<float> GetAdditiveModifiers(Stat stat);
        IEnumerable<float> GetPercentageModifiers(Stat stat);
    }
}