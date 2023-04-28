﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProgesorCreating.Core
{
    [Serializable]
    public class Condition
    {
        [SerializeField] private Disjunction[] and;

        public bool Check(IEnumerable<IPredicateEvaluator> evaluators)
        {
            foreach (Disjunction disjunction in and)
            {
                if (!disjunction.Check(evaluators))
                {
                    return false;
                }
            }

            return true;
        }
    }
}