using ProgesorCreating.Combat;
using ProgesorCreating.Inventories;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace ProgesorCreating.Editor 
{
    [CustomEditor(typeof(StatsEquipableItem))]
    public class EquipmentInspector : UnityEditor.Editor
    {
        public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
        {
            StatsEquipableItem example = (StatsEquipableItem)target;

            if (example == null || example.GetIcon() == null)
                return null;
            
            // example.PreviewIcon must be a supported format: ARGB32, RGBA32, RGB24,
            // Alpha8 or one of float formats
            Texture2D tex = new Texture2D (width, height);
            EditorUtility.CopySerialized (example.GetIcon().texture, tex);

            return tex;
        }
    }
}