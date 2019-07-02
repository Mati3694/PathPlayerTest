using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ShowIfAttribute))]
public class ShowIfDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ShowIfAttribute showIfAttrib = (ShowIfAttribute)attribute;

        if (showIfAttrib.condition)
            return EditorGUI.GetPropertyHeight(property, label);
        else
            return 0;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ShowIfAttribute showIfAttrib = (ShowIfAttribute)attribute;

        if (showIfAttrib.condition)
            EditorGUI.PropertyField(position, property, label, true);
    }
}
