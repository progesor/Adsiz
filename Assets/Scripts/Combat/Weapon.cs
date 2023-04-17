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

        public void Spawn(Transform handTransform, Animator animator)
        {
            if (equippedPrefab != null)
            {
                Instantiate(equippedPrefab, handTransform);
            }

            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
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