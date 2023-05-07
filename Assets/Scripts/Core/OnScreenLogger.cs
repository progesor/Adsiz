using System;
using System.Text;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Core
{
    public class OnScreenLogger : MonoBehaviour
    {
        private struct  LogEntry
        {
            public string _text { get; private set; }
            public LogType _type { get; private set; }
            public float _killTime { get; private set; }

            public void SetLogEntry(string text, LogType logType, float duration = 1f)
            {
                _text = text;
                _type = logType;
                _killTime = duration;
            }

            public void Reset()
            {
                _killTime = -1f;
            }
        }
        
        [Header("Settings")] 
        [SerializeField, Tooltip("Max amount of log entries.")]
        private int capacity = 10;

        [SerializeField, Tooltip("In seconds.")]
        private float onScreenDuration = 1f;

        public bool showLogType = true;
        public bool showStackTrace;
        
        [Header("Color")]
        #region Colors
        
        public Color backgroundColor=Color.gray;
        public Color textColor=Color.white;
        public Color logColor=Color.white;
        public Color warningColor=Color.yellow;
        public Color errorColor=Color.red;
        public Color exceptionColor=Color.red;

        #endregion

        private GUIStyle _backgroundStyle = new GUIStyle();
        private LogEntry[] _logEntries;

        private void Awake()
        {
            _backgroundStyle.normal.background=Texture2D.whiteTexture;
            _backgroundStyle.richText = true;
            _logEntries = new LogEntry[capacity];
        }
        private void OnEnable()
        {
            Application.logMessageReceivedThreaded += HandleLog;
        }

        private void OnDisable()
        {
            UnityEngine.Device.Application.logMessageReceivedThreaded -= HandleLog;
        }

        private void OnGUI()
        {
            Color previousColor = GUI.contentColor;

            GUI.backgroundColor = backgroundColor;
            GUILayout.BeginVertical(_backgroundStyle);
            for (int i = 0; i < _logEntries.Length; i++)
            {
                if (_logEntries[i]._killTime<Time.time)
                {
                    continue;
                }
                GUILayout.Label(_logEntries[i]._text);
            }
            GUILayout.EndVertical();

            GUI.contentColor = previousColor;
        }

        private void HandleLog(string logString, string stackTrace, LogType type)
        {
            if (_logEntries[_logEntries.Length-1]._killTime>Time.time)
            {
                _logEntries[0].Reset();
            }
            
            Array.Sort(_logEntries,SortEntryComparison);
            int index = Array.FindIndex(_logEntries, AvailableEntryComparison);

            BuildString(logString,stackTrace,type,out string text);
            _logEntries[index].SetLogEntry(text,type,onScreenDuration);

        }

        private void BuildString(in string logString, in string stackTrace, in LogType type, out string finalText)
        {
            StringBuilder text = new StringBuilder();
            if (showLogType)
            {
                text.Append("<color=#");
                text.Append(ColorUtility.ToHtmlStringRGBA(GetLogTypeColor(type)));
                text.Append(">");
                text.Append(type.ToString());
                text.Append(": </color>");
                text.Append("\t");
            }
            
            text.Append("<color=#");
            text.Append(ColorUtility.ToHtmlStringRGBA(textColor));
            text.Append(">");
            text.Append(logString);

            if (showStackTrace)
            {
                text.Append(logString);
                text.Append(stackTrace);
            }
            else
            {
                text.Append(logString);
            }
            
            text.Append("</color>");

            finalText = text.ToString();
        }

        private Color GetLogTypeColor(in LogType type)
        {
            switch (type)
            {
                case LogType.Log: return logColor;
                case LogType.Warning: return warningColor;
                case LogType.Error: return errorColor;
                default: return exceptionColor;
            }
        }
        private static bool AvailableEntryComparison(LogEntry entry)
        {
            return entry._killTime < Time.time;
        }
        private static int SortEntryComparison(LogEntry a, LogEntry b)
        {
            if (a._killTime<Time.time)
            {
                if (b._killTime<Time.time)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                if (b._killTime<Time.time)
                {
                    return -1;
                }
                else
                {
                    return a._killTime.CompareTo(b._killTime);
                }
            }
        }
    }
}