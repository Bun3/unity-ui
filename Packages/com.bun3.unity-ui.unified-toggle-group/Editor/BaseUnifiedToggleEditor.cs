using UnityEditor;
using UnityEngine;

/// <summary>
/// BaseUnifiedToggle을 상속하는 모든 클래스에 적용되는 커스텀 에디터입니다.
/// </summary>
[CustomEditor(typeof(BaseUnifiedToggle), true)]
public class BaseUnifiedToggleEditor : Editor
{
    private SerializedProperty _script;
    private SerializedProperty _authorGroup;
    private SerializedProperty _options;
    
    private void OnEnable()
    {
        _script = serializedObject.FindProperty("m_Script");
        _authorGroup = serializedObject.FindProperty("_authorGroup");
        _options = serializedObject.FindProperty("_options");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUI.enabled = false;
        EditorGUILayout.PropertyField(_script);
        GUI.enabled = true;
        
        var prevGroup = _authorGroup.objectReferenceValue;
        EditorGUILayout.PropertyField(_authorGroup);
        
        if (prevGroup != _authorGroup.objectReferenceValue)
        {
            if (prevGroup is UnifiedToggleGroup oldGroup)
            {
                oldGroup.Unregister((BaseUnifiedToggle)target);
            }

            if (_authorGroup.objectReferenceValue is UnifiedToggleGroup newGroup)
            {
                newGroup.Register((BaseUnifiedToggle)target);
            }
        }
        
        EditorGUI.BeginChangeCheck();
        
        EditorGUILayout.PropertyField(_options, true);

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
            
            if (_authorGroup.objectReferenceValue is UnifiedToggleGroup group)
            {
                Undo.RecordObject(target, "Sync Options");
                group.UpdateValues();

                EditorUtility.SetDirty(target);
                if (PrefabUtility.IsPartOfPrefabInstance(target))
                    PrefabUtility.RecordPrefabInstancePropertyModifications(target);

                serializedObject.Update();

                Repaint();
            }
        }
        
        serializedObject.ApplyModifiedProperties();

        DrawPropertiesExcluding(serializedObject, "_authorGroup", "_options", "m_Script");
    }
    
}
