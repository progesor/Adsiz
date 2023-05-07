using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Control
{
    public class PatrolPath : MonoBehaviour
    {
        private const float WaypointGizmoRadius = 0.3f;
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.DrawSphere(GetWaypoint(i),WaypointGizmoRadius);

                transform.GetChild(i).name = "Waypoint " + i;

                GUI.color=Color.black;
               // Handles.Label(GetWaypointTextPosition(i), i.ToString());
               Handles.Label(GetWaypointTextPosition(i), transform.GetChild(i).name);
                
                Gizmos.DrawLine(GetWaypoint(i),GetWaypoint(GetNextIndex(i)));
            }
        }
#endif

        private int GetNextIndex(int i)
        {
            if (i+1==transform.childCount)
            {
                return 0;
            }
            return i + 1;
        }

        private Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }

        private Vector3 GetWaypointTextPosition(int i)
        {
            Vector3 waypointPosition = GetWaypoint(i);
            Vector3 waypointTextPosition =
                new Vector3(waypointPosition.x, waypointPosition.y + 0.6f, waypointPosition.z);
            return waypointTextPosition;
        }
    }
}