using System;
using ProgesorCreating.Quests;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.UI.Quests
{
    public class QuestTooltipUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private Transform objectiveContainer;
        [SerializeField] private GameObject objectivePrefab;
        [SerializeField] private GameObject objectiveIncompletePrefab;
        [SerializeField] private TextMeshProUGUI rewardTextObject;
        public void Setup(QuestStatus status)
        {
            Quest quest = status.GetQuest();
            title.text = quest.GetTitle();

            foreach (Transform transform in objectiveContainer.transform)
            {
                Destroy(transform.gameObject);
            }

            foreach (Objective objective in quest.GetObjectives())
            {
                GameObject prefab = objectiveIncompletePrefab;
                if (status.IsObjectiveComplete(objective.reference))
                {
                    prefab = objectivePrefab;
                }
                
                GameObject objectiveInstance = Instantiate(prefab, objectiveContainer);
                TextMeshProUGUI objectiveText = objectiveInstance.GetComponentInChildren<TextMeshProUGUI>();
                objectiveText.text = objective.description;
            }

            rewardTextObject.text = GetRewardText(quest);

        }

        private string GetRewardText(Quest quest)
        {
            string rewardText=String.Empty;
            foreach (Reward reward in quest.GetRewards())
            {
                if (rewardText!=String.Empty)
                {
                    rewardText += ", ";
                }

                if (reward.Number>1)
                {
                    rewardText += reward.Number + " ";
                }
                rewardText += reward.Item.GetDisplayName();
            }

            if (rewardText ==String.Empty)
            {
                rewardText = "No reward";
            }

            rewardText += ".";
            return rewardText;
        }
    }
}