using ProgesorCreating.RPG.Combat;
using ProgesorCreating.RPG.Movement;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private void Update()
        {
            if (InteractWithCombat())return;

            if (InteractWithMovement())return;
            print("End of World");
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(ScreenPointToRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target);
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
                    GetComponent<Mover>().MoveTo(hit.point);
                }

                return true;
            }

            return false;
        }

        private static Ray ScreenPointToRay()
        {
            return Camera.main!.ScreenPointToRay(Input.mousePosition);
        }
    }

}
