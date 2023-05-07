using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Abilities
{
    public class AbilityData
    {
        private GameObject _user;
        private Vector3 _targetedPoint;
        private IEnumerable<GameObject> _targets;

        public AbilityData(GameObject user)
        {
            _user = user;
        }

        public void SetTargets(IEnumerable<GameObject> targets)
        {
            _targets = targets;
        }

        public void SetTargetedPoint(Vector3 targetedPoint)
        {
            _targetedPoint = targetedPoint;
        }

        public GameObject GetUser()
        {
            return _user;
        }

        public IEnumerable<GameObject> GetTargets()
        {
            return _targets;
        }

        public Vector3 GetTargetedPoint()
        {
            return _targetedPoint;
        }

        public void StartCoroutine(IEnumerator coroutine)
        {
            _user.GetComponent<MonoBehaviour>().StartCoroutine(coroutine);
        }
    }
}