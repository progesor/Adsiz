﻿using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] private AnimatorOverrideController animatorOverride;
        [SerializeField] private GameObject weaponPrefab;

        public void Spawn(Transform handTransform,Animator animator)
        {
            Instantiate(weaponPrefab, handTransform);
            animator.runtimeAnimatorController = animatorOverride;
        }
    }
}