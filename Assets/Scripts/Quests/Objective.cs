using System;
using ProgesorCreating.Utils;

namespace ProgesorCreating.Quests
{
    [Serializable]
    public class Objective
    {
        public string reference;
        public string description;
        public bool usesCondition;
        public Condition CompletiCondition;
    }
}