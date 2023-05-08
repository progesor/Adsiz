using System;
using ProgesorCreating.Attributes;
using ProgesorCreating.Combat;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Abilities.Effects
{
    [CreateAssetMenu(fileName = "Spawn Projectile Effect",menuName = "Abilities/Effect/New Spawn Projectile Effect",order = 0)]
    public class SpawnProjectileEffect : EffectStrategy
    {
        [SerializeField] private Projectile projectileToSpawn;
        [SerializeField] private float damage;
        [SerializeField] private bool isRightHand = true;
        public override void StartEffect(AbilityData data, Action finished)
        {
            Fighter fighter = data.GetUser().GetComponent<Fighter>();
            Vector3 spawnPosition = fighter.GetHandTransform(isRightHand).position;

            foreach (GameObject target in data.GetTargets())
            {
                Health health = target.GetComponent<Health>();
                if (health)
                {
                    Projectile projectile = Instantiate(projectileToSpawn);
                    projectile.transform.position = spawnPosition;
                    projectile.SetTarget(health, data.GetUser(), damage);
                }
                finished();
            }
        }
    }
}