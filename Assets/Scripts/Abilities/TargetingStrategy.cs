using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Abilities
{
    public abstract class TargetingStrategy : ScriptableObject
    {
        public abstract void StartTargeting(GameObject user);
    }
}