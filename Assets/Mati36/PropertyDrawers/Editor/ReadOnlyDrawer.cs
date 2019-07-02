using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ReadOnlyAttribute readOnlyAttrib = (ReadOnlyAttribute) attribute;

        if (readOnlyAttrib.onlyInPlay && !Application.isPlaying)
            EditorGUI.PropertyField(position, property);
        else
        {
            bool guiEnabled = GUI.enabled;
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property);
            GUI.enabled = guiEnabled;
        }
    }
}