using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(CustomLabelAttribute))]
public class CustomLabelDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var customLabelAttrib = (CustomLabelAttribute)attribute;
        ////label.text = customLabelAttrib.labelName;
        //EditorGUI.PrefixLabel(position, new GUIContent(customLabelAttrib.labelName));
        //Rect textPos = position;
        //textPos.x += EditorGUIUtility.labelWidth;
        //textPos.width -= EditorGUIUtility.labelWidth;

        EditorGUI.PropertyField(position, property, new GUIContent(customLabelAttrib.labelName));
    }
}
