using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProgesorCreating.Core
{
    [Serializable]
    public class Condition
    {
        [SerializeField] private string predicate;
        [SerializeField] private string[] parameters;

        public bool Check(IEnumerable<IPredicateEvaluator> evaluators)
        {
            foreach (IPredicateEvaluator evaluator in evaluators)
            {
                bool? result = evaluator.Evaluate(predicate, parameters);
                if (result==null)
                {
                    continue;
                }

                if (result == false) return false;
            }

            return true;
        }
    }
}