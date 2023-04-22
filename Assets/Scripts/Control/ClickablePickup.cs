using ProgesorCreating.Inventories;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Control
{
    [RequireComponent(typeof(Pickup))]
    public class ClickablePickup :MonoBehaviour, IRaycastable
    {
        private Pickup _pickup;

        private void Awake()
        {
            _pickup = GetComponent<Pickup>();
        }

        public CursorType getCursorType()
        {
            if (_pickup.CanBePickedUp())
            {
                return CursorType.Pickup;
            }
            else
            {
                return CursorType.FullPickup;
            }
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _pickup.PickupItem();
            }

            return true;
        }
    }
}