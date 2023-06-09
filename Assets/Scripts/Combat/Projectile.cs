﻿using ProgesorCreating.Attributes;
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
        private Vector3 _targetPoint;

        //private CapsuleCollider _targetCapsule;

        private void Start()
        {
            //_targetCapsule = _target.GetComponent<CapsuleCollider>();
            transform.LookAt(GetAimLocation());
        }

        private void Update()
        {
            if (_target != null && isHoming && !_target.IsDead())
            {
                transform.LookAt(GetAimLocation());
            }
            transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        }

        public void SetTarget(Health target, GameObject instigator, float damage)
        {
            SetTarget(instigator, damage, target);
        }

        public void SetTarget(Vector3 targetPoint, GameObject instigator, float damage)
        {
            SetTarget(instigator, damage, null, targetPoint);
        }

        public void SetTarget(GameObject instigator, float damage, Health target = null, Vector3 targetPoint = default)
        {
            _target = target;
            _targetPoint = targetPoint;
            _damage = damage;
            _instigator = instigator;

            Destroy(gameObject, maxLifeTime);
        }

        private Vector3 GetAimLocation()
        {
            if (_target == null)
            {
                return _targetPoint;
            }

            CapsuleCollider targetCapsule = _target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null)
            {
                return _target.transform.position;
            }

            return _target.transform.position + Vector3.up * targetCapsule.height / 2;
        }

        private void OnTriggerEnter(Collider other)
        {
            Health health = other.GetComponent<Health>();
            if (_target != null && health != _target) return;
            if (health == null || health.IsDead()) return;
            if (other.gameObject == _instigator) return;
            
            health.TakeDamage(_instigator, _damage);

            speed = 0;
            
            onHit.Invoke();

            if (hitEffect != null) Instantiate(hitEffect, GetAimLocation(), transform.rotation);

            foreach (var toDestroy in destroyOnHit) Destroy(toDestroy);

            Destroy(gameObject, lifeAfterImpact);
        }
    }
}