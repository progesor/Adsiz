using System;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Abilities.Filters
{
    [CreateAssetMenu(fileName = "Tag Filter",menuName = "Abilities/Filter/New Tag Filter",order = 0)]
    public class TagFilter : FilterStrategy
    {
        [SerializeField] private string tagToFilter;
        public override IEnumerable<GameObject> Filter(IEnumerable<GameObject> objectsToFilter)
        {
            foreach (GameObject gameObject in objectsToFilter)
            {
                if (gameObject.CompareTag(tagToFilter))
                {
                    yield return gameObject;
                }
            }
        }
    }
}