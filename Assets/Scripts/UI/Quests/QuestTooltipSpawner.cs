using ProgesorCreating.Core.UI.Tooltips;
using ProgesorCreating.Quests;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.UI.Quests
{
    public class QuestTooltipSpawner : TooltipSpawner
    {
        public override void UpdateTooltip(GameObject tooltip)
        {
            QuestStatus status = GetComponent<QuestItemUI>().GetQuestStatus();
            tooltip.GetComponent<QuestTooltipUI>().Setup(status);
        }

        public override bool CanCreateTooltip()
        {
            return true;
        }
    }
}