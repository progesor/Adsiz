using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float speed = 1;
        private CapsuleCollider _targetCapsule;

        private void Start()
        {
            _targetCapsule = target.GetComponent<CapsuleCollider>();
        }

        private void Update()
        {
            //used instead of target == null
            if (ReferenceEquals(target,null)) return;
            
            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        }

        private Vector3 GetAimLocation()
        {
            //used _targetCapsule of target == null
            if (ReferenceEquals(_targetCapsule,null))
            {
                return target.position;
            }

            return target.position + Vector3.up * _targetCapsule.height / 2;
        }
    }
}