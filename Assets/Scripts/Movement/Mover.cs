using System;
using ProgesorCreating.RPG.Attributes;
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

        [Serializable]
        struct MoverSaveData
        {
            public SerializableVector3 position;
            public SerializableVector3 rotation;
        }

        // public object CaptureState()
        // {
        //     Dictionary<string, object> data = new Dictionary<string, object>();
        //     data["position"] = new SerializableVector3(transform.position);
        //     data["rotation"] = new SerializableVector3(transform.eulerAngles);
        //     return data;
        // }
        
        public object CaptureState()
        {
            MoverSaveData data = new MoverSaveData();
            Transform transformData = transform;
            data.position = new SerializableVector3(transformData.position);
            data.rotation = new SerializableVector3(transformData.eulerAngles);
            return data;
        }

        // public void RestoreState(object state)
        // {
        //     Dictionary<string, object> data = (Dictionary<string, object>)state;
        //     _navMeshAgent.enabled = false;
        //     transform.position = ((SerializableVector3)data["position"]).ToVector();
        //     transform.eulerAngles = ((SerializableVector3)data["rotation"]).ToVector();
        //     _navMeshAgent.enabled = true;
        // }
        
        public void RestoreState(object state)
        {
            MoverSaveData data = (MoverSaveData)state;
            _navMeshAgent.enabled = false;
            transform.position = data.position.ToVector();
            transform.eulerAngles = data.rotation.ToVector();
            _navMeshAgent.enabled = true;
        }
    }

}
