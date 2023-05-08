using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Abilities.Effects
{
    [CreateAssetMenu(fileName = "Orient To Target Effect",menuName = "Abilities/Effect/New Orient To Target Effect",order = 0)]
    public class OrientToTargetEffect : EffectStrategy
    {
        public override void StartEffect(AbilityData data, Action finished)
        {
            data.GetUser().transform.LookAt(data.GetTargetedPoint());
            finished();
        }
    }
}