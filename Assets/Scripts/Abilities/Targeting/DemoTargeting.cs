using UnityEngine;

namespace ProgesorCreating.Abilities.Targeting
{
    [CreateAssetMenu(fileName = "Demo Targeting",menuName = "Abilities/Targeting/New Demo",order = 0)]
    public class DemoTargeting : TargetingStrategy
    {
        public override void StartTargeting(GameObject user)
        {
            Debug.Log("Demo Targeting Started");
        }
    }
}