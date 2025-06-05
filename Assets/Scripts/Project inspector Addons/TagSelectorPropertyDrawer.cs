
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(TagSelectorAttribute))]
public class TagSelectorPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType == SerializedPropertyType.String)
        {
            EditorGUI.BeginProperty(position, label, property);
            string[] tags = UnityEditorInternal.InternalEditorUtility.tags;
            int index = System.Array.IndexOf(tags, property.stringValue);
            index = EditorGUI.Popup(position, label.text, index, tags);
            property.stringValue = index >= 0 ? tags[index] : "";
            EditorGUI.EndProperty();
        }
        else
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }
}