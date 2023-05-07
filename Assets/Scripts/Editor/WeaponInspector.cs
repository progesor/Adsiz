using ProgesorCreating.Combat;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Editor
{
    [CustomEditor(typeof(WeaponConfig))]
    public class WeaponInspector : UnityEditor.Editor
    {
        [FormerlySerializedAs("m_InspectorXML")] public VisualTreeAsset mInspectorXML;
        
        // public override void OnInspectorGUI()
        // {
        //     WeaponConfig e = (WeaponConfig)target;
        //
        //     EditorGUI.BeginChangeCheck();
        //
        //     // Example has a single arg called PreviewIcon which is a Texture2D
        //     e.icon = (Sprite)
        //         EditorGUILayout.ObjectField(
        //             "Thumbnail",                    // string
        //             e.icon,                  // Texture2D
        //             typeof(Sprite),              // Texture2D object, of course
        //             false                           // allowSceneObjects
        //         );
        //
        //     if (EditorGUI.EndChangeCheck())
        //     {
        //         EditorUtility.SetDirty(e);
        //         AssetDatabase.SaveAssets();
        //         Repaint();
        //     }
        // }
        
        public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
        {
            WeaponConfig example = (WeaponConfig)target;

            if (example == null || example.GetIcon() == null)
                return null;
            
            // example.PreviewIcon must be a supported format: ARGB32, RGBA32, RGB24,
            // Alpha8 or one of float formats
            Texture2D tex = new Texture2D (width, height);
            EditorUtility.CopySerialized (example.GetIcon().texture, tex);

            return tex;
        }
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement myInspector = new VisualElement();
        
            mInspectorXML.CloneTree(myInspector);
            
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