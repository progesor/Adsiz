using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Combat
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private UnityEvent onHit;
        public void OnHit()
        {
            onHit.Invoke();
        }
    }
}