using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Core
{
    public class DestroyAfterEffect : MonoBehaviour
    {
        [SerializeField] private float destroyTime = 1f;
        [SerializeField] private GameObject targetToDestroy;

        // private void Start()
        // {
        //     Destroy(gameObject, destroyTime);
        // }
        
        private void Update()
        {
            if (!GetComponent<ParticleSystem>().IsAlive())
            {
                Destroy(targetToDestroy != null ? targetToDestroy : gameObject, destroyTime);
            }
        }
    }
}