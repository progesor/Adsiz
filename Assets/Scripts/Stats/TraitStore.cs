using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Stats
{
    public class TraitStore : MonoBehaviour
    {
        private Dictionary<Trait, int> _assignedPoints = new Dictionary<Trait, int>();
        private Dictionary<Trait, int> _stagedPoints = new Dictionary<Trait, int>();

        private int _unassignedPoints = 10;

        public int GetProposedPoints(Trait trait)
        {
            return GetPoints(trait) + GetStagedPoints(trait);
        }

        public int GetPoints(Trait trait)
        {
            return _assignedPoints.ContainsKey(trait) ? _assignedPoints[trait] : 0;
        }

        public int GetStagedPoints(Trait trait)
        {
            return _stagedPoints.ContainsKey(trait) ? _stagedPoints[trait] : 0;
        }

        public void AssignPoints(Trait trait, int points)
        {
            if (!CanAssignPoints(trait,points))return;
            
            _stagedPoints[trait] = GetStagedPoints(trait) + points;
            _unassignedPoints -= points;
        }

        public bool CanAssignPoints(Trait trait, int points)
        {
            if (GetStagedPoints(trait) + points < 0) return false;
            if (_unassignedPoints < points) return false;
            return true;
        }

        public int GetUnassignedPoints()
        {
            return _unassignedPoints;
        }

        public void Commit()
        {
            foreach (Trait trait in _stagedPoints.Keys)
            {
                _assignedPoints[trait] = GetProposedPoints(trait);
            }
            _stagedPoints.Clear();
        }
    }
}