using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProgesorCreating.Core
{
    [Serializable]
    public class Predicate
    {
        [SerializeField] private string predicate;
        [SerializeField] private string[] parameters;
        [SerializeField] private bool negate = false;

        public bool Check(IEnumerable<IPredicateEvaluator> evaluators)
        {
            foreach (IPredicateEvaluator evaluator in evaluators)
            {
                bool? result = evaluator.Evaluate(predicate, parameters);
                if (result==null)
                {
                    continue;
                }

                if (result == negate) return false;
            }

            return true;
        }
    }
}