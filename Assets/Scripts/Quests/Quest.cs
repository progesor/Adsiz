using System;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Quests
{
    [CreateAssetMenu(fileName = "Quest",menuName = "Quests/New Quest", order = 0)]
    public class Quest : ScriptableObject
    {
        [SerializeField] private List<Objective> objectives = new List<Objective>();
        [SerializeField] private List<Reward> rewards = new List<Reward>();

        public string GetTitle()
        {
            return name;
        }

        public int GetObjectiveCount()
        {
            return objectives.Count;
        }

        public IEnumerable<Objective> GetObjectives()
        {
            return objectives;
        }

        public bool HasObjective(string objectiveRef)
        {
            foreach (var objective in objectives)
            {
                if (objective.reference==objectiveRef)
                {
                    return true;
                }
            }

            return false;
        }

        public static Quest GetByName(string questName)
        {
            foreach (Quest quest in Resources.LoadAll<Quest>(""))
            {
                if (quest.name==questName)
                {
                    return quest;
                }
            }

            return null;
            
        }
    }
}