using System;
using ProgesorCreating.RPG.Core;
using ProgesorCreating.RPG.Saving;
using UnityEngine;
using UnityEngine.AI;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Movement
{
    public class Mover : MonoBehaviour,IAction,ISaveable
    {
        [SerializeField] public Transform target;
        [SerializeField] private float maxSpeed = 4.1f;

        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private Health _health;
        private static readonly int ForwardSpeed = Animator.StringToHash("forwardSpeed");

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _health = GetComponent<Health>();
        }

        void Update()
        {
            _navMeshAgent.enabled = !_health.IsDead();
            
            UpdateAnimator();
        }
    
        private void UpdateAnimator()
        {
            Vector3 velocity = _navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float forwardSpeed = localVelocity.z;
            _animator.SetFloat(ForwardSpeed,forwardSpeed);
        }

        public void StartMovementAction(Vector3 destination,float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }
    
        public void MoveTo(Vector3 destination, float speedFraction)
        {
            _navMeshAgent.destination = destination;
            _navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            _navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            _navMeshAgent.isStopped = true;
        }
        
        public void FootL()
        {
        
        }
        public void FootR()
        {
        
        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3)state;
            _navMeshAgent.enabled = false;
            transform.position = position.ToVector();
            _navMeshAgent.enabled = true;
        }
    }

}
