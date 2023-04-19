using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Stats
{
    public interface IModifierProvider
    {
        IEnumerable<float> GetAdditiveModifier(Stat stat);
    }
}