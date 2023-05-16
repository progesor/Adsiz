using ProgesorCreating.Abilities;
using UnityEditor;
using UnityEngine;

namespace ProgesorCreating.Editor
{
    [CustomEditor(typeof(Ability))]
    public class AbilityInspector : UnityEditor.Editor
    {
        public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
        {
            Ability example = (Ability)target;

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