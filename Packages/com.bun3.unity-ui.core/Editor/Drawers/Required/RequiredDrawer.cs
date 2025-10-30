using Core.Attributes.Required;
using UnityEditor;
using UnityEngine;

namespace Core.Editor.Drawers.Required
{
    [CustomPropertyDrawer(typeof(RequiredAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label);
            
            if (property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue == null)
            {
                var requiredAttribute = (RequiredAttribute)attribute;
                if (requiredAttribute.autoCache)
                {
                    var go = property.serializedObject.targetObject as GameObject;
                    go ??= (property.serializedObject.targetObject as Component)?.gameObject;
                    property.objectReferenceValue = go?.GetComponent(fieldInfo.FieldType);
                    
                    if (property.objectReferenceValue != null)
                    {
                        property.serializedObject.ApplyModifiedProperties();
                        return;
                    }
                }
                
                
                var rect = new Rect(position);
                rect.y += EditorGUIUtility.singleLineHeight + 4;
                rect.height = EditorGUIUtility.singleLineHeight * 2;

                GUILayout.Space(rect.height + 4);
                
                var message = requiredAttribute.message;
                var style = new GUIStyle(EditorStyles.helpBox)
                {
                    alignment = TextAnchor.MiddleLeft,
                    richText = true
                };

                GUI.Label(rect, new GUIContent()
                {
                    text = $"{property.displayName} is required.\n{message}",
                    tooltip = message,
                    image = EditorGUIUtility.IconContent("console.erroricon").image
                }, style);
            }
            
        }
    }
}
