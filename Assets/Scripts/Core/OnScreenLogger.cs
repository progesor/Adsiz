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
            public string Text { get; private set; }
            public LogType Type { get; private set; }
            public float KillTime { get; private set; }

            public void SetLogEntry(string text, LogType logType, float duration = 1f)
            {
                Text = text;
                Type = logType;
                KillTime = duration;
            }

            public void Reset()
            {
                KillTime = -1f;
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
                if (_logEntries[i].KillTime<Time.time)
                {
                    continue;
                }
                GUILayout.Label(_logEntries[i].Text);
            }
            GUILayout.EndVertical();

            GUI.contentColor = previousColor;
        }

        private void HandleLog(string logString, string stackTrace, LogType type)
        {
            if (_logEntries[_logEntries.Length-1].KillTime>Time.time)
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
            return entry.KillTime < Time.time;
        }
        private static int SortEntryComparison(LogEntry a, LogEntry b)
        {
            if (a.KillTime<Time.time)
            {
                if (b.KillTime<Time.time)
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
                if (b.KillTime<Time.time)
                {
                    return -1;
                }
                else
                {
                    return a.KillTime.CompareTo(b.KillTime);
                }
            }
        }
    }
}