using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using ZLinq;

namespace UnifiedToggle
{
    public partial class UnifiedToggleGroup
    {
        [ContextMenu(nameof(UpdateValues))]
        public void UpdateValues()
        {
            SetOptionValues(_presets);
        }
    
        public void SetOptionValues(string[] values)
        {
            foreach (var toggle in _toggles)
                toggle.SetOptionValues(values);
        }
    
        public void Register(BaseUnifiedToggle toggle)
        {
            if (toggle == null)
                return;
            if (_toggles.Contains(toggle)) 
                return;
            Array.Resize(ref _toggles, _toggles.Length + 1);
            _toggles[^1] = toggle;
            UpdateValues();
        }
    
        public void Unregister(BaseUnifiedToggle toggle)
        {
            if (toggle == null)
                return;
            if (!_toggles.Contains(toggle)) 
                return;
            _toggles = _toggles.Where(t => t != toggle).ToArray();
            UpdateValues();
        }

        public void EnsureValidToggles()
        {
            _toggles = _toggles.Where(x => x != null).ToArray();
        }
    
        static UnifiedToggleGroup()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowCallback;
        }

        public static void HierarchyWindowCallback(int instanceID, Rect selectionRect)
        {
            var go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            var group = go ? go.GetComponent<UnifiedToggleGroup>() : null;
            if (group == null) return;
            if (go.GetComponent<UnifiedToggleToggleGroup>()) return; // 기존 예외 유지

            var presets = group.GetPresets()?.ToArray() ?? Array.Empty<string>();
            if (presets.Length == 0) 
                return;

            var index = Array.IndexOf(presets, group.CurrentPreset);
            var currentLabel = index >= 0 ? presets[index] ?? "(None)" : "(Select)";

            // 하이어라키 우측 정렬된 작은 드롭다운 렉트
            var style = EditorStyles.miniPullDown;
            style.alignment = TextAnchor.MiddleRight;
        
            var size = style.CalcSize(new GUIContent(currentLabel));
            var width = Mathf.Max(68f, size.x + 12f);
            var rect = new Rect(selectionRect.xMax - width - 4f, selectionRect.y, width, selectionRect.height);

            // 그리기
            if (Event.current.type == EventType.Repaint)
                style.Draw(rect, new GUIContent(currentLabel), false, false, false, false);

            // 클릭 처리: 메뉴로 바꾸기(하이어라키에서도 안정적으로 동작)
            if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
            {
                var menu = new GenericMenu();
                for (var i = 0; i < presets.Length; i++)
                {
                    var capture = i;
                    var on = capture == index;
                    var label = presets[capture] ?? "(None)";
                    menu.AddItem(new GUIContent(label), on, () =>
                    {
                        group.SetValue(presets[capture]);
                        EditorUtility.SetDirty(group);
                        EditorApplication.RepaintHierarchyWindow();
                    });
                }
                menu.DropDown(rect);
                Event.current.Use();
            }
        }
    }
}