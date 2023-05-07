using System;
using ProgesorCreating.Control;
using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Editor
{
    [CustomEditor(typeof(Waypoints))]
    public class WaypointsEditor : UnityEditor.Editor
    {
        #region Fields
        
        
        private float _dashSize = 4f;
        private Waypoints _waypoints;

        private int[] _segmentIndices;
        private Vector3[] _lines;
        private SerializedProperty _pointProperty;

        private Color _pointOnLineColor=Color.green;
        private GUISkin _inspectorSkin;
        private GUISkin _sceneSkin;

        #endregion

        private void OnEnable()
        {
            _waypoints = target as Waypoints;
            _pointProperty = serializedObject.FindProperty("points");
            
            _sceneSkin = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Scene);
            _inspectorSkin = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector);
            Tools.hidden = true;
            
            CreateSegments();
            CreateLines();
        }

        private void OnDisable()
        {
            Tools.hidden = false;
        }

        public override void OnInspectorGUI()
        {
            //DrawDefaultInspector();
            //DrawPropertiesExcluding(serializedObject,"points");
            //base.OnInspectorGUI();
            string[] excludeList = new string[2];
            excludeList[0] = "points";
            excludeList[1] = "debug";
            DrawPropertiesExcluding(serializedObject,excludeList);
            
            GUILayout.Space(5f);
            GUILayout.Label("Waypoints");
            
            if (_waypoints.length==0)
            {
                if (GUILayout.Button(EditorGUIUtility.IconContent("d_Toolbar Plus"),GUILayout.ExpandWidth(true)))
                {
                    _pointProperty.InsertArrayElementAtIndex(0);
                    serializedObject.ApplyModifiedProperties();
                    var transform = SceneView.lastActiveSceneView.camera.transform;
                    _waypoints[0] = transform.position +
                                    transform.forward * 1.5f;
                    CreateSegments();
                    CreateLines();
                }
            }

            for (int i = 0; i < _waypoints.length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button(i.ToString(),_inspectorSkin.button,GUILayout.ExpandWidth(false)))
                {
                    SceneView.lastActiveSceneView.LookAt(_waypoints[i]);
                }
                
                EditorGUI.BeginChangeCheck();
                Vector3 value = EditorGUILayout.Vector3Field(String.Empty,_waypoints[i],GUILayout.ExpandWidth(true));
                if (EditorGUI.EndChangeCheck())
                {
                    _waypoints[i] = value;
                    Undo.RecordObject(_waypoints,"Moved waypoint.");
                    SceneView.RepaintAll();
                    EditorUtility.SetDirty(_waypoints);
                }

                if (GUILayout.Button(EditorGUIUtility.IconContent("d_Toolbar Plus"),GUILayout.ExpandWidth(false)))
                {
                    _pointProperty.InsertArrayElementAtIndex(i + 1);
                    serializedObject.ApplyModifiedProperties();

                    Vector3 midPoint;
                    if (_waypoints.length >= 2 && i + 1 >= _waypoints.length - 1)
                    {
                        Vector3 direction = (_waypoints[i] - _waypoints[i - 1]).normalized;
                        midPoint = _waypoints[i] + direction;
                    }
                    else if (i + 1 >= _waypoints.length - 1)
                    {
                        midPoint = _waypoints[i] + Vector3.right;
                    }
                    else
                    {
                        midPoint = (_waypoints[i] + _waypoints[i + 1]) * 0.5f;
                    }

                    _waypoints[i + 1] = midPoint;
                    CreateSegments();
                    CreateLines();
                }

                if (GUILayout.Button(EditorGUIUtility.IconContent("d_Toolbar Minus"),GUILayout.ExpandWidth(false)))
                {
                    _pointProperty.DeleteArrayElementAtIndex(i);
                    serializedObject.ApplyModifiedProperties();
                    CreateSegments();
                    CreateLines();
                    i--;
                }
                EditorGUILayout.EndHorizontal();
            }
            
            GUILayout.Space(5f);
            GUILayout.Label("Debug Menu");
            _waypoints.debug = EditorGUILayout.Toggle("Enable Debug", _waypoints.debug);
        }


        private void OnSceneGUI()
        {
            //Recreates segments if undo or redo
            if (Event.current.type == EventType.ValidateCommand &&
                Event.current.commandName.Equals("UndoRedoPerformed"))
            {
                CreateSegments();
                CreateLines();
            }

            //Position Handles
            for (int i = 0; i < _waypoints.length; i++)
            {
                Handles.Label(_waypoints[i] + (Vector3.down * 0.0f), i.ToString(), _sceneSkin.textField);
                EditorGUI.BeginChangeCheck();
                Vector3 newPosition = Handles.PositionHandle(_waypoints[i], Quaternion.identity);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(_waypoints,"Moved waypoint.");
                    _waypoints[i] = newPosition;
                }
                
            }

            DrawConnectedLines();
            DrawClosestPointOnLines();
        }
        
        private void DrawConnectedLines()
        {
            Handles.DrawDottedLines(_waypoints.points,_segmentIndices,_dashSize);
        }

        private void CreateSegments()
        {
            if (_waypoints.length<2)
            {
                return;
            }

            _segmentIndices = new int[_waypoints.length * 2];
            int index = 0;
            for (int start = 0; start < _segmentIndices.Length-2; start+=2)
            {
                _segmentIndices[start] = index;
                index++;
                _segmentIndices[start + 1] = index;
            }
            _segmentIndices[_segmentIndices.Length - 2] = _waypoints.length - 1;
            _segmentIndices[_segmentIndices.Length - 1] = 0;
        }
        private void CreateLines()
        {
            _lines = new Vector3[_waypoints.length + 1];
            Array.Copy(_waypoints.points,_lines,_waypoints.length);
            _lines[_lines.Length - 1] = _lines[0];
        }

        private void DrawClosestPointOnLines()
        {
            Event currentEvent = Event.current;
            if (currentEvent.modifiers==EventModifiers.Control)
            {
                Vector3 pointPosition = HandleUtility.ClosestPointToPolyLine(_lines);
                Color previousColor = Handles.color;
                Handles.color = _pointOnLineColor;

                Handles.DrawSolidDisc(pointPosition, Camera.current.transform.forward,
                    HandleUtility.GetHandleSize(pointPosition) * 0.1f);
                
                Handles.color = previousColor;
                HandleUtility.Repaint();

                if (currentEvent.type==EventType.MouseDown && currentEvent.button==0)
                {
                    GUIUtility.hotControl = 0;
                    int index = GetIndexOfClosestLine(pointPosition);
                    _pointProperty.InsertArrayElementAtIndex(index);
                    serializedObject.ApplyModifiedProperties();
                    _waypoints[index] = pointPosition;
                    CreateSegments();
                    CreateLines();
                    currentEvent.Use();
                }
            }
        }

        private int GetIndexOfClosestLine(Vector3 pointPosition)
        {
            float distance = float.MaxValue;
            int index = 0;
            for (int i = 0; i < _lines.Length - 1; i++)
            {
                float currentDistance = HandleUtility.DistancePointLine(pointPosition, _lines[i], _lines[i + 1]);
                if (currentDistance<distance)
                {
                    distance = currentDistance;
                    index = i;
                }
            }

            return index + 1;
        }
    }
}
