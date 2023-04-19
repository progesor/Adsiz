using ProgesorCreating.RPG.Attributes;
using ProgesorCreating.RPG.Combat;
using ProgesorCreating.RPG.Movement;
using UnityEngine;
using UnityEngine.EventSystems;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Fighter _fighter;
        private Mover _mover;
        private Health _health;
        private Camera _camera;

        [SerializeField] private CursorMapping[] cursorMappings;

        private void Awake()
        {
            _fighter = GetComponent<Fighter>();
            _mover = GetComponent<Mover>();
            _health = GetComponent<Health>();
            _camera=Camera.main;
        }

        private void Update()
        {
            if (InteractWithUI()) return;
            if (_health.IsDead()) 
            {
                SetCursor(CursorType.Death);
                return;
            }

            if (InteractWithComponent())return;
            if (InteractWithMovement())return;
            
            SetCursor(CursorType.None);
        }


        private bool InteractWithUI()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                SetCursor(CursorType.UI);
                return true;
            }
            return false;
        }
        
        
        private bool InteractWithComponent()
        {
            RaycastHit[] hits = Physics.RaycastAll(ScreenPointToRay());
            foreach (RaycastHit hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();

                foreach (IRaycastable raycastable in raycastables)
                {
                    if (raycastable.HandleRaycast(this))
                    {
                        SetCursor(raycastable.getCursorType());
                        return true;
                    }
                }
            }

            return false;
        }

        private bool InteractWithMovement()
        {
            bool hasHit = Physics.Raycast(ScreenPointToRay(), out var hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    _mover.StartMovementAction(hit.point, 1f);
                }
                SetCursor(CursorType.Movement);
                return true;
            }

            return false;
        }
        
        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if (mapping.type==type)
                {
                    return mapping;
                }
            }

            return cursorMappings[0];
        }

        private Ray ScreenPointToRay()
        {
            return _camera.ScreenPointToRay(Input.mousePosition);
            
        }
    }

}
