using System;
using ProgesorCreating.RPG.Core;
using Unity.VisualScripting;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed = 1;
        private CapsuleCollider _targetCapsule;
        
        private Health _target;
        private float _damage;

        private void Start()
        {
            _targetCapsule = _target.GetComponent<CapsuleCollider>();
        }

        private void Update()
        {
            //used instead of target == null
            if (ReferenceEquals(_target,null)) return;
            
            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        }

        public void SetTarget(Health target, float damage)
        {
            _target = target;
            _damage = damage;
        }

        private Vector3 GetAimLocation()
        {
            //used _targetCapsule of target == null
            if (ReferenceEquals(_targetCapsule,null))
            {
                return _target.transform.position;
            }

            return _target.transform.position + Vector3.up * _targetCapsule.height / 2;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>()!=_target)return;
            _target.TakeDamage(_damage);
            Destroy(gameObject);
        }

    }
}