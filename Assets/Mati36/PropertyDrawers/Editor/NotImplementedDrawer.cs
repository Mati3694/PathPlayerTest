using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(NotImplementedAttribute))]
public class NotImplementedDrawer : PropertyDrawer
{
    const string labelText = "NOT IMPLEMENTED YET";

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PrefixLabel(position, label);
        Rect textPos = position;
        textPos.x += EditorGUIUtility.labelWidth;
        textPos.width -= EditorGUIUtility.labelWidth;

        bool guiEnabled = GUI.enabled;
        GUI.enabled = false;
        EditorGUI.LabelField(textPos, labelText);
        GUI.enabled = guiEnabled;
    }
}
