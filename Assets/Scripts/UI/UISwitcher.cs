using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.UI
{
    public class UISwitcher : MonoBehaviour
    {
        [SerializeField] private GameObject entryPoint;

        private void Start()
        {
            SwitchTo(entryPoint);
        }

        public void SwitchTo(GameObject toDisplay)
        {
            if (toDisplay.transform.parent != transform) return;
            
            foreach (Transform chiled in transform)
            {
                chiled.gameObject.SetActive(chiled.gameObject==toDisplay);
            }
        }
    }
}