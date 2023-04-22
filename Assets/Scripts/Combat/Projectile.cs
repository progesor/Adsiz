using System;
using ProgesorCreating.Attributes;
using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed = 1;
        [SerializeField] private bool isHoming;
        [SerializeField] private GameObject hitEffect;
        [SerializeField] private float maxLifeTime = 10f;
        [SerializeField] private GameObject[] destroyOnHit;
        [SerializeField] private float lifeAfterImpact = 0.2f;
        [SerializeField] private UnityEvent onHit;
        
        private float _damage;
        private GameObject _instigator;
        private Health _target;

        private CapsuleCollider _targetCapsule;

        private void Start()
        {
            _targetCapsule = _target.GetComponent<CapsuleCollider>();
            transform.LookAt(GetAimLocation());
        }

        private void Update()
        {
            //used instead of target == null
            if (ReferenceEquals(_target, null)) return;

            if (isHoming && !_target.IsDead()) transform.LookAt(GetAimLocation());

            transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != _target) return;
            if (_target.IsDead()) return;
            _target.TakeDamage(_instigator, _damage);

            speed = 0;
            
            onHit.Invoke();

            if (hitEffect != null) Instantiate(hitEffect, GetAimLocation(), transform.rotation);

            foreach (var toDestroy in destroyOnHit) Destroy(toDestroy);

            Destroy(gameObject, lifeAfterImpact);
        }

        public void SetTarget(Health target,GameObject instigator, float damage)
        {
            _target = target;
            _damage = damage;
            _instigator = instigator;

            Destroy(gameObject, maxLifeTime);
        }

        private Vector3 GetAimLocation()
        {
            //used _targetCapsule of target == null
            if (ReferenceEquals(_targetCapsule, null)) return _target.transform.position;

            return _target.transform.position + Vector3.up * _targetCapsule.height / 2;
        }
    }
}