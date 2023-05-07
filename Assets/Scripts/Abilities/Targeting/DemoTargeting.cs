using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProgesorCreating.Abilities.Targeting
{
    [CreateAssetMenu(fileName = "Demo Targeting",menuName = "Abilities/Targeting/New Demo",order = 0)]
    public class DemoTargeting : TargetingStrategy
    {
        public override void StartTargeting(GameObject user,Action<IEnumerable<GameObject>> finished)
        {
            Debug.Log("Demo Targeting Started");
            finished(null);
        }
    }
}