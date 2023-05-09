using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Stats
{
    public class TraitStore : MonoBehaviour
    {
        private Dictionary<Trait, int> _assignedPoints = new Dictionary<Trait, int>();
        private Dictionary<Trait, int> _stagedPoints = new Dictionary<Trait, int>();

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
        }

        public bool CanAssignPoints(Trait trait, int points)
        {
            if (GetStagedPoints(trait) + points < 0) return false;
            if (GetUnassignedPoints() < points) return false;
            return true;
        }

        public int GetUnassignedPoints()
        {
            return GetAssignablePoints() - GetTotalProposedPoints();
        }

        public int GetTotalProposedPoints()
        {
            int total = 0;
            foreach (int points in _assignedPoints.Values)
            {
                total += points;
            }
            
            foreach (int points in _stagedPoints.Values)
            {
                total += points;
            }

            return total;
        }

        public void Commit()
        {
            foreach (Trait trait in _stagedPoints.Keys)
            {
                _assignedPoints[trait] = GetProposedPoints(trait);
            }
            _stagedPoints.Clear();
        }

        public int GetAssignablePoints()
        {
            return (int)GetComponent<BaseStats>().GetStat(Stat.TotalTraitPoints);
        }
    }
}