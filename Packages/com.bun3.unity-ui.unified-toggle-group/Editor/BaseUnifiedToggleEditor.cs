using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace UnifiedToggle.Editor
{
    /// <summary>
    /// BaseUnifiedToggle을 상속하는 모든 클래스에 적용되는 커스텀 에디터입니다.
    /// </summary>
#if ODIN_INSPECTOR
    [OdinDontUseOdinInspector]
#endif
    [CustomEditor(typeof(BaseUnifiedToggle), true)]
    public class BaseUnifiedToggleEditor : UnityEditor.Editor
    {
        protected SerializedProperty _script;
        protected SerializedProperty _authorGroup;
        protected SerializedProperty _options;
        
        protected BaseUnifiedToggle m_Toggle;

        private void OnEnable()
        {
            _script = serializedObject.FindProperty("m_Script");
            _authorGroup = serializedObject.FindProperty("_authorGroup");
            _options = serializedObject.FindProperty("_options");
            
            m_Toggle = target as BaseUnifiedToggle;
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
                OnOptionChanged();
                
                serializedObject.ApplyModifiedProperties();

                if (_authorGroup.objectReferenceValue is UnifiedToggleGroup group)
                {
                    Undo.RecordObject(target, "Sync Options");
                    group.UpdateValues();

                    serializedObject.Update();

                    Repaint();
                }
                
                if (!Application.isPlaying)
                    EditorSceneManager.MarkSceneDirty(m_Toggle.gameObject.scene);
            }

            serializedObject.ApplyModifiedProperties();

            DrawPropertiesExcluding(serializedObject, "_authorGroup", "_options", "m_Script");
        }

        protected virtual void OnOptionChanged()
        {
            
        }
    }
}