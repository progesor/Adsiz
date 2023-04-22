using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.UI.Damage_Text
{
    public class Destroyer : MonoBehaviour
    {
        [SerializeField] private GameObject targetToDestroy;
        public void DestroyText()
        {
            Destroy(targetToDestroy);
        }
    }
}