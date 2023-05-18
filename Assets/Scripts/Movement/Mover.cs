using System;
using ProgesorCreating.Attributes;
using ProgesorCreating.Core;
using ProgesorCreating.Saving;
using UnityEngine;
using UnityEngine.AI;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Movement
{
    public class Mover : MonoBehaviour,IAction,ISaveable
    {
        [SerializeField] public Transform target;
        [SerializeField] private float maxSpeed = 4.1f;
        [SerializeField] private float maxNavPathLength = 40f;

        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private Health _health;
        private static readonly int ForwardSpeed = Animator.StringToHash("forwardSpeed");

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _health = GetComponent<Health>();
        }
        void Update()
        {
            if (_health!=null)
            {
                _navMeshAgent.enabled = !_health.IsDead();
            }
            
            UpdateAnimator();
        }
    
        private void UpdateAnimator()
        {
            Vector3 velocity = _navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float forwardSpeed = localVelocity.z;
            _animator.SetFloat(ForwardSpeed,forwardSpeed);
        }
        
        private float GetPathLength(NavMeshPath path)
        {
            float total = 0;

            if (path.corners.Length < 2) return total;
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                total += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }

            return total;
        }

        public void StartMovementAction(Vector3 destination,float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public bool CanMoveTo(Vector3 destination)
        {
            NavMeshPath path = new NavMeshPath();
            bool hasPath = NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, path);
            if (!hasPath) return false;
            if (path.status != NavMeshPathStatus.PathComplete) return false;
            if (GetPathLength(path) > maxNavPathLength) return false;

            return true;
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
