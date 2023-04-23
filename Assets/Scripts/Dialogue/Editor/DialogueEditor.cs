using UnityEditor;
using UnityEditor.Callbacks;

namespace ProgesorCreating.Dialogue.Editor
{
    public class DialogueEditor : EditorWindow
    {
        [MenuItem("Window/Dialogue Editor")]
        public static void ShowEditorWindow()
        {
            GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");
        }

        [OnOpenAssetAttribute(1)]
        public static bool OnOpenAsset(int instanceId, int line)
        {
            Dialogue dialogue = EditorUtility.InstanceIDToObject(instanceId) as Dialogue;
            if (dialogue!=null)
            {
                ShowEditorWindow();
                return true;
            }
            return false;
        }
    }
}