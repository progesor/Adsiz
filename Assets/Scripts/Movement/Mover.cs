using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace ProgesorCreating.RPG.Control
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] public Transform target;
        private static readonly int ForwardSpeed = Animator.StringToHash("forwardSpeed");


        void Start()
        {
        
        }

        void Update()
        {
            UpdateAnimator();
        }
    
        private void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float forwardSpeed = localVelocity.z;
            GetComponent<Animator>().SetFloat(ForwardSpeed,forwardSpeed);
        }
    
        public void MoveTo(Vector3 destination)
        {
            GetComponent<NavMeshAgent>().destination = destination;
        }

        public void FootL()
        {
        
        }
        public void FootR()
        {
        
        }
    }

}
