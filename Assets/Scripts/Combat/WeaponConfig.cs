using System.Collections.Generic;
using ProgesorCreating.Attributes;
using ProgesorCreating.Inventories;
using ProgesorCreating.Stats;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class WeaponConfig : EquipableItem,IModifierProvider
    {
        [SerializeField] private AnimatorOverrideController animatorOverride;
        [SerializeField] private Weapon equippedPrefab;
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private bool isRightHanded = true;
        [SerializeField] private Projectile projectile;
        
        [SerializeField] private Modifier[] additiveModifiers;
        [SerializeField] private Modifier[] percentageModifiers;

        private const string WeaponName = "Weapon";

        public Weapon Spawn(Transform rightHand,Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);

            Weapon weapon = null;
            
            if (equippedPrefab != null)
            {
                Transform handTransform = GetTransform(rightHand, leftHand);
                weapon = Instantiate(equippedPrefab, handTransform);
                weapon.gameObject.name = WeaponName;
            }
            
            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
            else if (overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController;
            }

            return weapon;
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(WeaponName);
            if (oldWeapon==null)
            {
                oldWeapon = leftHand.Find(WeaponName);
            }

            if (oldWeapon==null)return;

            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);
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

        public void LaunchProjectile(Transform rightHand,Transform leftHand,Health target,GameObject instigator,float calculatedDamage)
        {
            Projectile projectileInstance =
                Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target,instigator, calculatedDamage);
        }
        
        public float GetRange()
        {
            return weaponRange;
        }

        // public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        // {
        //     if (stat == Stat.Damage)
        //     {
        //         yield return weaponDamage;
        //     }
        // }
        //
        // public IEnumerable<float> GetPercentageModifiers(Stat stat)
        // {
        //     if (stat == Stat.Damage)
        //     {
        //         yield return percentageBonus;
        //     }
        // }
        
        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            foreach (var modifier in additiveModifiers)
            {
                if (modifier.stat==stat)
                {
                    yield return modifier.value;
                }
            }
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            foreach (var modifier in percentageModifiers)
            {
                if (modifier.stat==stat)
                {
                    yield return modifier.value;
                }
            }
        }
    }
}