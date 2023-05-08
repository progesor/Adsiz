using System.Collections.Generic;
using ProgesorCreating.Attributes;
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
        [SerializeField] private float cooldownTime;
        [SerializeField] private float manaCost;

        public override void Use(GameObject user)
        {
            Mana mana = user.GetComponent<Mana>();

            if (mana.GetMana()<manaCost)
            {
                return;
            }
            
            CooldownStore cooldownStore = user.GetComponent<CooldownStore>();
            if (cooldownStore.GetTimeRemaining(this)>0)
            {
                return;
            }
            
            AbilityData data = new AbilityData(user);
            targetingStrategy.StartTargeting(data, ()=> TargetAcquired(data));
        }

        private void TargetAcquired(AbilityData data)
        {
            Mana mana = data.GetUser().GetComponent<Mana>();
            if (!mana.UseMana(manaCost))return;

            CooldownStore cooldownStore = data.GetUser().GetComponent<CooldownStore>();
            cooldownStore.StartCooldown(this,cooldownTime);
            
            foreach (FilterStrategy filterStrategy in filterStrategies)
            {
                data.SetTargets(filterStrategy.Filter(data.GetTargets()));
            }

            foreach (EffectStrategy effect in effectStrategies)
            {
                effect.StartEffect(data, EffectFinished);
            }
        }

        private void EffectFinished()
        {
            
        }
    }
}