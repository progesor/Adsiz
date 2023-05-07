using System;
using ProgesorCreating.Attributes;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Abilities.Effects
{
    [CreateAssetMenu(fileName = "Health Effect",menuName = "Abilities/Effect/New Health Effect",order = 0)]
    public class HealthEffect : EffectStrategy
    {
        [SerializeField] private float healthChange;
        public override void StartEffect(AbilityData data, Action finished)
        {
            foreach (GameObject target in data.GetTargets())
            {
                Health health = target.GetComponent<Health>();
                if (health)
                {
                    if (healthChange < 0)
                    {
                        health.TakeDamage(data.GetUser(), -healthChange);
                    }
                    else
                    {
                        health.Heal(healthChange);
                    }

                }
            }
            finished();
        }
    }
}