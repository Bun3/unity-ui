using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public partial class BaseUnifiedToggle
{
        
    static BaseUnifiedToggle()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowCallback;
    }

    public static void HierarchyWindowCallback(int instanceID, Rect selectionRect)
    {
        var go = (GameObject)EditorUtility.InstanceIDToObject(instanceID);
        var toggle = go?.GetComponent<BaseUnifiedToggle>();
        if (toggle == null)
            return;

        var rect = new Rect(
            GUILayoutUtility.GetLastRect().width - selectionRect.height - (selectionRect.width * 0.5f),
            selectionRect.y,
            selectionRect.width * 0.5f,
            selectionRect.height
        );

        var currentPreset = toggle._authorGroup?.CurrentPreset;
        currentPreset ??= "No Preset";

        if (go.GetComponent<UnifiedToggleGroup>())
        {
            if (go.TryGetComponent<UnifiedToggleToggleGroup>(out var groupToggle))
            {
                currentPreset += $" ({groupToggle.Group.CurrentPreset})";
            }
            else
            {
                return;
            }
        }
        
        EditorGUI.LabelField(rect, currentPreset, new GUIStyle()
        {
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleRight,
            clipping = TextClipping.Overflow,
        });
    }
}

public partial class BaseUnifiedToggle<TComponent>
{
    protected virtual void OnValidate()
    {
        var set = new HashSet<Type>();
        for (var i = 0; i < _options.Count; i++)
        {
            var option = _options[i];
            if (option == null)
                continue;
            
            var type = option.GetType();
            if (!set.Add(type))
            {
                EditorUtility.DisplayDialog(
                    "중복된 옵션 형식 감지",
                    "옵션 형식 " + type.Name + " 이(가) 중복되었습니다. 동일한 옵션 형식은 하나만 허용됩니다.",
                    "확인"
                );
                
                _options[i] = null;
            }
        }
    }
}