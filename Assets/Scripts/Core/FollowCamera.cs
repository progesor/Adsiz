using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProgesorCreating.RPG.Movement
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

