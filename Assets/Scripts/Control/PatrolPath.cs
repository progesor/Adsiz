using System;
using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        private const float waypointGizmoRadius = 0.3f;
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.DrawSphere(GetWaypoint(i),waypointGizmoRadius);

                transform.GetChild(i).name = "Waypoint " + i;

                GUI.color=Color.black;
               // Handles.Label(GetWaypointTextPossition(i), i.ToString());
               Handles.Label(GetWaypointTextPossition(i), transform.GetChild(i).name);
                
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

        private Vector3 GetWaypointTextPossition(int i)
        {
            Vector3 waypointPossition = GetWaypoint(i);
            Vector3 waypointTextPossition =
                new Vector3(waypointPossition.x, waypointPossition.y + 0.6f, waypointPossition.z);
            return waypointTextPossition;
        }
    }
}