using System;
using System.Collections;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Abilities.Effects
{
    [CreateAssetMenu(fileName = "Spawn Target Prefab Effect",menuName = "Abilities/Effect/New Spawn Target Prefab Effect",order = 0)]
    public class SpawnTargetPrefabEffect : EffectStrategy
    {
        [SerializeField] private Transform prefabToSpawn;
        [SerializeField] private float destroyDelay = -1;
        [SerializeField] private float areaEffectRadius;
        public override void StartEffect(AbilityData data, Action finished)
        {
            data.StartCoroutine(Effect(data, finished));
        }

        private IEnumerator Effect(AbilityData data, Action finished)
        {
            Transform instace = Instantiate(prefabToSpawn);
            instace.position = data.GetTargetedPoint();
            instace.localScale = new Vector3(areaEffectRadius, areaEffectRadius, areaEffectRadius );
            if (destroyDelay>0)
            {
                yield return new WaitForSeconds(destroyDelay);
                Destroy(instace.gameObject);
            }

            finished();
        }
    }
}