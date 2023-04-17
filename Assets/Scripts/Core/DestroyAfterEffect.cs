using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Core
{
    public class DestroyAfterEffect : MonoBehaviour
    {
        // private void Update()
        // {
        //     if (!GetComponent<ParticleSystem>().IsAlive())
        //     {
        //         Destroy(gameObject);
        //     }
        // }
        
        [SerializeField] private float destroyTime = 1f;

        private void Start()
        {
            Destroy(gameObject, destroyTime);
        }
    }
}