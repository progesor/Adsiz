using System;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Core
{
    [Serializable]
    public class Disjunction
    {
        [SerializeField] private Predicate[] or;

        public bool Check(IEnumerable<IPredicateEvaluator> evaluators)
        {
            foreach (Predicate predicate in or)
            {
                if (predicate.Check(evaluators))
                {
                    return true;
                }
            }

            return false;
        }
    }
}