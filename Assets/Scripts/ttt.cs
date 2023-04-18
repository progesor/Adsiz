using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    public class ttt
    {
        [MenuItem("Examples/Set Custom Icon on GameObject")]
        public static void SetCustomIconOnGameObject()
        {
            var go = new GameObject();
            var icon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/MyIcon.png");

            EditorGUIUtility.SetIconForObject(go, icon);
        }
        
        [MenuItem("Examples/Set Custom Icon on MonoScript")]
        public static void SetCustomIconOnMonoScript()
        {
            var monoImporter = AssetImporter.GetAtPath("Assets/MyMonoBehaviour.cs") as MonoImporter;
            var monoScript = monoImporter.GetScript();
            var icon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/MyIcon.png");

            EditorGUIUtility.SetIconForObject(monoScript, icon);
        }
    }
}