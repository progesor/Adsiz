using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Abilities.Effects
{
    [CreateAssetMenu(fileName = "Trigger Animation Effect",menuName = "Abilities/Effect/New Trigger Animation Effect",order = 0)]
    public class TriggerAnimationEffect : EffectStrategy
    {
        [SerializeField] private string animationTriggers;

        public override void StartEffect(AbilityData data, Action finished)
        {
            Animator animator = data.GetUser().GetComponent<Animator>();
            animator.SetTrigger(animationTriggers);
            finished();
        }
    }
}