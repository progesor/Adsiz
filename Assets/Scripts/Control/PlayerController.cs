using ProgesorCreating.RPG.Combat;
using ProgesorCreating.RPG.Core;
using ProgesorCreating.RPG.Movement;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Fighter _fighter;
        private Mover _mover;
        private Health _health;
        private Camera _camera;

        private void Start()
        {
            _fighter = GetComponent<Fighter>();
            _mover = GetComponent<Mover>();
            _health = GetComponent<Health>();
            _camera=Camera.main;
        }

        private void Update()
        {
            if (_health.IsDead())return;
            
            if (InteractWithCombat())return;

            if (InteractWithMovement())return;
            print("End of World");
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(ScreenPointToRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target==null)continue;

                
                if (!GetComponent<Fighter>().CanAttack(target.gameObject))
                {
                    continue;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    _fighter.Attack(target.gameObject);
                }

                return true;
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
                    _mover.StartMovementAction(hit.point);
                }

                return true;
            }

            return false;
        }

        private Ray ScreenPointToRay()
        {
            return _camera.ScreenPointToRay(Input.mousePosition);
            
        }
    }

}
