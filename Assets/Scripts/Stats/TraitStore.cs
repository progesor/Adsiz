using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Stats
{
    public class TraitStore : MonoBehaviour
    {
        private Dictionary<Trait, int> _assignedPoints = new Dictionary<Trait, int>();

        private int _unassignedPoints = 10;

        public int GetPoints(Trait trait)
        {
            return _assignedPoints.ContainsKey(trait) ? _assignedPoints[trait] : 0;
        }

        public void AssignPoints(Trait trait, int points)
        {
            if (!CanAssignPoints(trait,points))return;
            
            _assignedPoints[trait] = GetPoints(trait) + points;
            _unassignedPoints -= points;
        }

        public bool CanAssignPoints(Trait trait, int points)
        {
            if (GetPoints(trait) + points < 0) return false;
            if (_unassignedPoints < points) return false;
            return true;
        }

        public int GetUnassignedPoints()
        {
            return _unassignedPoints;
        }
    }
}