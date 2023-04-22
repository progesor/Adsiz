using UnityEngine;
using UnityEngine.AI;

namespace ProgesorCreating.Inventories
{
    public class RandomDropper : ItemDropper
    {
        //CONFIG DATA
        [Tooltip("How far can the pickups be scattered from the dropper.")] 
        [SerializeField] private float scatterDistance = 1;
        [SerializeField] private InventoryItem[] dropLibrary;
        [SerializeField] private int numberOfDrops = 2;
        
        //CONSTANTS
        private const int Attempts = 30;

        public void RandomDrop()
        {
            for (int i = 0; i < numberOfDrops; i++)
            {
                var item = dropLibrary[Random.Range(0, dropLibrary.Length)];
                DropItem(item,1);
            }
        }
        protected override Vector3 GetDropLocation()
        {
            for (int i = 0; i < Attempts; i++)
            {
                Vector3 randomPoint = transform.position + (Random.insideUnitSphere * scatterDistance);
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint,out hit, 0.1f,NavMesh.AllAreas))
                {
                    return hit.position;
                }
            }
            return transform.position;
        }
    }
}