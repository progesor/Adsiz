using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Health healthComponent;
        [SerializeField] private Slider sliderComponent;
        [SerializeField] private Canvas rootCanvas;

        private void Update()
        {
            float healthPoint = healthComponent.GetFraction();
            if (Mathf.Approximately(healthPoint,0))
            {
                rootCanvas.enabled = false;
            }
            else if (Mathf.Approximately(healthPoint,1))
            {
                rootCanvas.enabled = false;
            }
            else
            {
                rootCanvas.enabled = true;
                sliderComponent.value = healthPoint;
            }
            
        }
    }
}