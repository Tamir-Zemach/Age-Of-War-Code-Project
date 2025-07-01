﻿
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(TagSelectorAttribute))]
public class TagSelectorPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        TagSelectorAttribute tagSelector = (TagSelectorAttribute)attribute;
        GUIContent dynamicLabel = new GUIContent(label.text, tagSelector.Tooltip);

        if (property.propertyType == SerializedPropertyType.String)
        {
            EditorGUI.BeginProperty(position, dynamicLabel, property);

            // Draw the label manually
            position = EditorGUI.PrefixLabel(position, label);

            string[] tags = UnityEditorInternal.InternalEditorUtility.tags;
            int index = System.Array.IndexOf(tags, property.stringValue);
            index = EditorGUI.Popup(position, index, tags);
            property.stringValue = index >= 0 ? tags[index] : "";

            EditorGUI.EndProperty();
        }
        else
        {
            EditorGUI.PropertyField(position, property, dynamicLabel);
        }
    }
}
