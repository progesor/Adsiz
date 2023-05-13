using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.UI
{
    public class SaveLoadUI : MonoBehaviour
    {
        [SerializeField] private Transform contentRoot;
        [SerializeField] private GameObject buttonPrefab;

        private void OnEnable()
        {
            foreach (Transform child in contentRoot)
            {
                Destroy(child.gameObject);
            }

            Instantiate(buttonPrefab, contentRoot);
            Instantiate(buttonPrefab, contentRoot);
            Instantiate(buttonPrefab, contentRoot);
        }
    }
}