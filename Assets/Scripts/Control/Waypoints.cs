using System;
using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Control
{
    public class Waypoints : MonoBehaviour
    {
        public Vector3[] points;

        [SerializeField] public bool debug;
        

        public Vector3 this[int index]
        {
            get { return points[index]; }
            set { points[index] = value; }
        }

        /// <summary>
        /// Returns number of waypoints.
        /// </summary>
        public int Length
        {
            get { return points.Length; }
        }

        public int GetClosestWaypoint(Vector3 position)
        {
            float closestDistance = float.MaxValue;
            int closestIndex = -1;
            for (int i = 0; i < points.Length; i++)
            {
                float distance = Vector3.Distance(position, points[i]);
                if (distance<closestDistance)
                {
                    closestDistance = distance;
                    closestIndex = i;
                }
            }

            return closestIndex;
        }

        private void OnDrawGizmos()
        {
            if (debug)
            {
                GUISkin _sceneSkin = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Scene);
                for (int i = 0; i < points.Length; i++)
                {
                    Handles.Label(points[i] + (Vector3.down * 0.0f), i.ToString(), _sceneSkin.textField);
                    Gizmos.DrawSphere(points[i], 0.15f);
                
                    //Gizmos.DrawLine(points[i],points[GetNextIndex(i)]);
                    Handles.DrawDottedLine(points[i],points[GetNextIndex(i)],4f);
                }
            }
        }
        
        private int GetNextIndex(int i)
        {
            if (i+1==points.Length)
            {
                return 0;
            }
            return i + 1;
        }
    }
}
