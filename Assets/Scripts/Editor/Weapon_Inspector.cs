using System.IO;
using ProgesorCreating.Combat;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProgesorCreating.Editor
{
    [CustomEditor(typeof(WeaponConfig))]
    public class Weapon_Inspector : UnityEditor.Editor
    {
        public VisualTreeAsset m_InspectorXML;
        
        public override void OnInspectorGUI()
        {
            WeaponConfig e = (WeaponConfig)target;

            EditorGUI.BeginChangeCheck();

            // Example has a single arg called PreviewIcon which is a Texture2D
            e.imageIcon = (Texture2D)
                EditorGUILayout.ObjectField(
                    "Thumbnail",                    // string
                    e.imageIcon,                  // Texture2D
                    typeof(Texture2D),              // Texture2D object, of course
                    false                           // allowSceneObjects
                );

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(e);
                AssetDatabase.SaveAssets();
                Repaint();
            }
        }
        
        public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
        {
            WeaponConfig example = (WeaponConfig)target;

            if (example == null || example.imageIcon == null)
                return null;

            // example.PreviewIcon must be a supported format: ARGB32, RGBA32, RGB24,
            // Alpha8 or one of float formats
            Texture2D tex = new Texture2D (width, height);
            EditorUtility.CopySerialized (example.imageIcon, tex);

            return tex;
        }
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement myInspector = new VisualElement();

            m_InspectorXML.CloneTree(myInspector);
            
            // Get a reference to the default inspector foldout control
            VisualElement inspectorFoldout = myInspector.Q("Default_Inspector");
            
            // Attach a default inspector to the foldout
            InspectorElement.FillDefaultInspector(inspectorFoldout, serializedObject, this);
            
            //return base.CreateInspectorGUI();
            
            return myInspector;
        }

        // public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
        // {
        //     WeaponConfig weaponConfig = (WeaponConfig)target;
        //     if (weaponConfig == null || weaponConfig.imageIcon == null) return null;
        //     
        //     Texture2D tex = new Texture2D (width, height);
        //     EditorUtility.CopySerialized(weaponConfig.imageIcon,tex);
        //     
        //     //return base.RenderStaticPreview(assetPath, subAssets, width, height);
        //     return tex;
        // }
    }
}