using ProgesorCreating.RPG.Combat;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProgesorCreating.RPG.Editor
{
    [CustomEditor(typeof(Weapon))]
    public class Weapon_Inspector : UnityEditor.Editor
    {
        public VisualTreeAsset m_InspectorXML;

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

        public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
        {
            Weapon weapon = (Weapon)target;
            if (weapon == null || weapon.imageIcon == null) return null;
            
            Texture2D tex = new Texture2D (width, height);
            EditorUtility.CopySerialized(weapon.imageIcon,tex);
            
            //return base.RenderStaticPreview(assetPath, subAssets, width, height);
            return tex;
        }
    }
}