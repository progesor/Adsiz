using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;
    
        void LateUpdate()
        {
            transform.position = target.position;
        }
    }
}

