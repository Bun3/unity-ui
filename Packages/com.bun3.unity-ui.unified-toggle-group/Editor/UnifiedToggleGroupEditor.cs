using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UnifiedToggleGroup), true)]
public class UnifiedToggleGroupEditor : Editor
{
    private SerializedProperty _presetsProperty;
    private SerializedProperty _togglesProperty;

    private void OnEnable()
    {
        _presetsProperty = serializedObject.FindProperty("_presets");
        _togglesProperty = serializedObject.FindProperty("_toggles");
    }

    public override void OnInspectorGUI()
    {
        var group = (UnifiedToggleGroup)target;

        serializedObject.Update();

        EditorGUI.BeginChangeCheck();
        
        EditorGUILayout.PropertyField(_presetsProperty, true);

        using (new EditorGUI.DisabledScope(true))
        {
            EditorGUILayout.PropertyField(_togglesProperty, true);
        }
        
        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
            group.UpdateValues();
            MarkAllDirty(group);
        }
        
        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.Space();
        DrawButtons(group);
    }

    private void MarkAllDirty(UnifiedToggleGroup group)
    {
        EditorUtility.SetDirty(group);
        if (group.GetToggles() == null) return;
        
        foreach (var toggle in group.GetToggles())
        {
            if (toggle != null)
            {
                EditorUtility.SetDirty(toggle);
            }
        }
    }

    private void DrawButtons(UnifiedToggleGroup group)
    {
        var changed = false;
        EditorGUILayout.BeginHorizontal();
        
        var presets = group.GetPresets();
        if (presets == null) return;
        
        foreach (var value in presets)
        {
            var origin = GUI.color;
            GUI.color = group.CurrentPreset == value ? Color.green : Color.white;
            if (GUILayout.Button(value, GUILayout.Height(30)))
            {
                changed = true;
                group.SetValue(value);
            }
        
            GUI.color = origin;
        }
        EditorGUILayout.EndHorizontal();
        
        if (changed)
        {
            MarkAllDirty(group);
        }
    }
}

