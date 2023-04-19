using ProgesorCreating.RPG.Attributes;
using ProgesorCreating.RPG.Control;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRaycastable
    {
        public bool HandleRaycast(PlayerController callingController)
        {
            if (!callingController.GetComponent<Fighter>().CanAttack(gameObject))
            {
                return false;
            }

            if (Input.GetMouseButton(0))
            {
                callingController.GetComponent<Fighter>().Attack(gameObject);
            }
            
            return true;
        }
    }
}