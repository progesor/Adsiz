using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Abilities
{
    public class AbilityData
    {
        private GameObject _user;
        private IEnumerable<GameObject> _targets;

        public AbilityData(GameObject user)
        {
            _user = user;
        }

        public void SetTargets(IEnumerable<GameObject> targets)
        {
            _targets = targets;
        }

        public IEnumerable<GameObject> GetTargets()
        {
            return _targets;
        }

        public GameObject GetUser()
        {
            return _user;
        }
    }
}