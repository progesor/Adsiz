using System;
using System.Collections.Generic;
using ProgesorCreating.Attributes;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Abilities.Effects
{
    [CreateAssetMenu(fileName = "Health Effect",menuName = "Abilities/Effect/New Health Effect",order = 0)]
    public class HealthEffect : EffectStrategy
    {
        [SerializeField] private float healthChange;
        public override void StartEffect(GameObject user, IEnumerable<GameObject> targets, Action finished)
        {
            foreach (GameObject target in targets)
            {
                Health health = target.GetComponent<Health>();
                if (health)
                {
                    if (healthChange < 0)
                    {
                        health.TakeDamage(user, -healthChange);
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