using System.Collections;
using System.Collections.Generic;
using ProgesorCreating.RPG.Control;
using UnityEngine;

namespace ProgesorCreating.RPG.Core
{
    public class PlayerController : MonoBehaviour
    {
        void Start()
        {
        
        }

        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }
    
        private void MoveToCursor()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);

            if (hasHit)
            {
                GetComponent<Mover>().MoveTo(hit.point);
            }
        }
    }

}
