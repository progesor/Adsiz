using System.Collections.Generic;
using ProgesorCreating.Inventories;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Abilities
{
    [CreateAssetMenu(fileName = "Ability",menuName = "Abilities/New Ability",order = 0)]
    public class Ability : ActionItem
    {
        [SerializeField] private TargetingStrategy targetingStrategy;
        [SerializeField] private FilterStrategy[] filterStrategies;
        [SerializeField] private EffectStrategy[] effectStrategies;

        public override void Use(GameObject user)
        {
            targetingStrategy.StartTargeting(user, targets =>
            {
                TargetAcquired(user, targets);
            });
        }

        private void TargetAcquired(GameObject user, IEnumerable<GameObject> targets)
        {
            foreach (FilterStrategy filterStrategy in filterStrategies)
            {
                targets = filterStrategy.Filter(targets);
            }

            foreach (EffectStrategy effect in effectStrategies)
            {
                effect.StartEffect(user, targets, EffectFinished);
            }
        }

        private void EffectFinished()
        {
            
        }
    }
}