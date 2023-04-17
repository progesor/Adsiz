using ProgesorCreating.RPG.Core;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] private AnimatorOverrideController animatorOverride;
        [SerializeField] private GameObject equippedPrefab;
        [SerializeField] private float weaponDamage = 5f;
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private bool isRightHanded = true;
        [SerializeField] private Projectile projectile;

        public void Spawn(Transform rightHand,Transform leftHand, Animator animator)
        {
            if (equippedPrefab != null)
            {
                Transform handTransform = GetTransform(rightHand, leftHand);
                Instantiate(equippedPrefab, handTransform);
            }

            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }

        private Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform = isRightHanded ? rightHand : leftHand;
            return handTransform;
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void LaunchProjectile(Transform rightHand,Transform leftHand,Health target)
        {
            Projectile projectileInstance =
                Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, weaponDamage);
        }

        public float GetDamage()
        {
            return weaponDamage;
        }
        
        public float GetRange()
        {
            return weaponRange;
        }
    }
}