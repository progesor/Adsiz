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

        public override void Use(GameObject user)
        {
            targetingStrategy.StartTargeting(user,TargetAcquired);
        }

        private void TargetAcquired(IEnumerable<GameObject> targets)
        {
            Debug.Log("Target Acquired");
            foreach (FilterStrategy filterStrategy in filterStrategies)
            {
                targets = filterStrategy.Filter(targets);
            }
            
            foreach (GameObject target in targets)
            {
                Debug.Log(target);
            }
        }
    }
}