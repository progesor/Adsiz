using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Health healthComponent;
        [SerializeField] private Slider sliderComponent;
        [SerializeField] private Canvas rootCanvas;

        private void Update()
        {
            float healtPoint = healthComponent.GetFraction();
            if (Mathf.Approximately(healtPoint,0))
            {
                rootCanvas.enabled = false;
            }
            else if (Mathf.Approximately(healtPoint,1))
            {
                rootCanvas.enabled = false;
            }
            else
            {
                rootCanvas.enabled = true;
                sliderComponent.value = healtPoint;
            }
            
        }
    }
}