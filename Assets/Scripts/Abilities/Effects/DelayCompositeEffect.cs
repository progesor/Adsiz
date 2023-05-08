using System;
using System.Collections;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Abilities.Effects
{
    [CreateAssetMenu(fileName = "Delay Composite Effect",menuName = "Abilities/Effect/New Delay Composite Effect",order = 0)]
    public class DelayCompositeEffect : EffectStrategy
    {
        [SerializeField] private float delay;
        [SerializeField] private EffectStrategy[] delayedEffects;
        public override void StartEffect(AbilityData data, Action finished)
        {
            data.StartCoroutine(DelayedEffects(data,finished));
        }

        private IEnumerator DelayedEffects(AbilityData data, Action finished)
        {
            yield return new WaitForSeconds(delay);
            foreach (EffectStrategy effect in delayedEffects)
            {
                effect.StartEffect(data, finished);
            }
        }
    }
}