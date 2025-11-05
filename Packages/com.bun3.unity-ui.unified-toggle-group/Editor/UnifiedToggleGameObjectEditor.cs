using UnityEditor;
using UnityEngine;

namespace UnifiedToggle.Editor
{
    [CustomEditor(typeof(UnifiedToggleGameObject), true)]
    public class UnifiedToggleGameObjectEditor : BaseUnifiedToggleEditor
    {
        protected override void OnOptionChanged()
        {
            base.OnOptionChanged();
            
            //Undo.RecordObject(target, "Edit Options");
            //serializedObject.ApplyModifiedProperties();
            
        }
    }

}

