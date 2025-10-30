using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

public partial class UnifiedToggleGroup
{
    [ContextMenu(nameof(FindAll))]
    private void FindAll()
    {
        var childGroups = GetComponentsInChildren<UnifiedToggleGroup>()
            .Where(c => c.gameObject != gameObject);

        var ignores = childGroups.SelectMany(c => c._toggles);

        _toggles = GetComponentsInChildren<BaseUnifiedToggle>()
            .Except(ignores)
            .ToArray();
    }
    
    [ContextMenu(nameof(UpdateValues))]
    public void UpdateValues()
    {
        SetOptionValues(_presets);
    }
    
    public void Register(BaseUnifiedToggle toggle)
    {
        if (_toggles.Contains(toggle)) 
            return;
        Array.Resize(ref _toggles, _toggles.Length + 1);
        _toggles[^1] = toggle;
        UpdateValues();
    }
    
    public void Unregister(BaseUnifiedToggle toggle)
    {
        if (!_toggles.Contains(toggle)) 
            return;
        _toggles = _toggles.Where(t => t != toggle).ToArray();
        UpdateValues();
    }
    
    static UnifiedToggleGroup()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowCallback;
    }

    public static void HierarchyWindowCallback(int instanceID, Rect selectionRect)
    {
        var go = (GameObject)EditorUtility.InstanceIDToObject(instanceID);
        var group = go?.GetComponent<UnifiedToggleGroup>();
        if (group == null)
            return;

        if (go.GetComponent<UnifiedToggleToggleGroup>())
            return;

        var rect = new Rect(
            GUILayoutUtility.GetLastRect().width - selectionRect.height - (selectionRect.width * 0.5f),
            selectionRect.y,
            selectionRect.width * 0.5f,
            selectionRect.height
        );

        var prev = group.CurrentPreset;
        var presets = group.GetPresets().ToArray();
        var index = presets.ToList().IndexOf(prev);

        var style = EditorStyles.popup;
        style.alignment = TextAnchor.MiddleRight;
        
        var nextIndex = EditorGUI.Popup(rect, index, presets, style);
        if (index != nextIndex)
        {
            group.SetValue(presets[nextIndex]);
            EditorUtility.SetDirty(group);
            HandleUtility.Repaint();
        }
    }
}