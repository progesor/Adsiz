using System;
using ProgesorCreating.Attributes;
using ProgesorCreating.Inventories;
using ProgesorCreating.Movement;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CursorMapping[] cursorMappings;
        [SerializeField] private float maxNavMeshProjectionDistance = 1f;
        [SerializeField] private float raycastRadius = 0.5f;
        [SerializeField] private int numberOfAbilities = 5;

        private bool _isDraggingUI;
        
        private Mover _mover;
        private Health _health;
        private static Camera _camera;
        private ActionStore _actionStore;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _health = GetComponent<Health>();
            _camera=Camera.main;
            _actionStore = GetComponent<ActionStore>();
        }

        private void Update()
        {
            if (InteractWithUI()) return;
            if (_health.IsDead()) 
            {
                SetCursor(CursorType.Death);
                return;
            }

            UseAbilities();

            if (InteractWithComponent())return;
            if (InteractWithMovement())return;
            
            SetCursor(CursorType.None);
        }


        private bool InteractWithUI()
        {
            if (Input.GetMouseButtonUp(0))
            {
                _isDraggingUI = false;
            }
            
            if (EventSystem.current.IsPointerOverGameObject())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _isDraggingUI = true;
                }
                SetCursor(CursorType.UI);
                return true;
            }

            if (_isDraggingUI)
            {
                return true;
            }
            return false;
        }

        private void UseAbilities()
        {
            for (int i = 0; i < numberOfAbilities; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1+i))
                {
                    _actionStore.Use(i, gameObject);
                }
            }
            
        }
        
        
        private bool InteractWithComponent()
        {
            RaycastHit[] hits = RaycastAllSorted();
            foreach (RaycastHit hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach (IRaycastable raycastable in raycastables)
                {
                    if (raycastable.HandleRaycast(this))
                    {
                        SetCursor(raycastable.GetCursorType());
                        return true;
                    }
                }
            }

            return false;
        }

        RaycastHit[] RaycastAllSorted()
        {
            RaycastHit[] hits = Physics.SphereCastAll(GetMouseRay(), raycastRadius);
            //RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            float[] distances = new float[hits.Length];
            for (int i = 0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }
            Array.Sort(distances, hits);
            return hits;
        }

        private bool InteractWithMovement()
        {
            // bool hasHit = Physics.Raycast(GetMouseRay(), out var hit);
            bool hasHit = RaycastNavMesh(out var target);
            if (hasHit)
            {
                if (!_mover.CanMoveTo(target)) return false;
                
                if (Input.GetMouseButton(0))
                {
                    _mover.StartMovementAction(target, 1f);
                }
                SetCursor(CursorType.Movement);
                return true;
            }

            return false;
        }

        private bool RaycastNavMesh(out Vector3 target)
        {
            target = new Vector3();

            bool hasHit = Physics.Raycast(GetMouseRay(), out var hit);
            if (!hasHit) return false;

            bool hasCastToNavMesh = NavMesh.SamplePosition(hit.point, out var navMeshHit, maxNavMeshProjectionDistance,
                NavMesh.AllAreas);

            if (!hasCastToNavMesh) return false;

            target = navMeshHit.position;
          
            return true;
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

        public static Ray GetMouseRay()
        {
            return _camera.ScreenPointToRay(Input.mousePosition);
            
        }
    }

}
