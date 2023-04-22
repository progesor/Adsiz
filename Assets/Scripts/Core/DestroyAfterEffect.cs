using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Core
{
    public class DestroyAfterEffect : MonoBehaviour
    {
        [SerializeField] private float destroyTime = 1f;
        [SerializeField] private GameObject targetToDestroy = null;

        // private void Start()
        // {
        //     Destroy(gameObject, destroyTime);
        // }
        
        private void Update()
        {
            if (!GetComponent<ParticleSystem>().IsAlive())
            {
                if (targetToDestroy!=null)
                {
                    Destroy(targetToDestroy,destroyTime);
                }
                else
                {
                    Destroy(gameObject,destroyTime);
                }
                
            }
        }
    }
}