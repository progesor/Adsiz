using System;
using System.Collections.Generic;
using ProgesorCreating.Saving;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Stats
{
    public class TraitStore : MonoBehaviour, IModifierProvider,ISaveable
    {
        [SerializeField] private TraitBonus[] bonusConfig;
        
        private Dictionary<Trait, int> _assignedPoints = new Dictionary<Trait, int>();
        private Dictionary<Trait, int> _stagedPoints = new Dictionary<Trait, int>();

        private Dictionary<Stat, Dictionary<Trait, float>> _additiveBonusCache;
        private Dictionary<Stat, Dictionary<Trait, float>> _percentageBonusCache;

        private void Awake()
        {
            _additiveBonusCache = new Dictionary<Stat, Dictionary<Trait, float>>();
            _percentageBonusCache = new Dictionary<Stat, Dictionary<Trait, float>>();
            foreach (TraitBonus bonus in bonusConfig)
            {
                if (!_additiveBonusCache.ContainsKey(bonus.stat))
                {
                    _additiveBonusCache[bonus.stat] = new Dictionary<Trait, float>();
                }
                if (!_percentageBonusCache.ContainsKey(bonus.stat))
                {
                    _percentageBonusCache[bonus.stat] = new Dictionary<Trait, float>();
                }

                _additiveBonusCache[bonus.stat][bonus.trait] = bonus.additiveBonusPerPoint;
                _percentageBonusCache[bonus.stat][bonus.trait] = bonus.percentageBonusPerPoint;
            }
        }

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

        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            if (!_additiveBonusCache.ContainsKey(stat))yield break;

            foreach (Trait trait in _additiveBonusCache[stat].Keys)
            {
                float bonus = _additiveBonusCache[stat][trait];
                yield return bonus * GetPoints(trait);
            }
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            if (!_percentageBonusCache.ContainsKey(stat))yield break;

            foreach (Trait trait in _percentageBonusCache[stat].Keys)
            {
                float bonus = _percentageBonusCache[stat][trait];
                yield return bonus * GetPoints(trait);
            }
        }

        public object CaptureState()
        {
            return _assignedPoints;
        }

        public void RestoreState(object state)
        {
            _assignedPoints = new Dictionary<Trait, int>((IDictionary<Trait, int>)state);
        }
    }
}