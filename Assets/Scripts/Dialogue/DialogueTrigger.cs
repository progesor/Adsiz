using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private string action;
        [SerializeField] private UnityEvent onTrigger;

        public void Trigger(string actionToTrigger)
        {
            if (actionToTrigger==action)
            {
                onTrigger.Invoke();
            }
        }
    }
}