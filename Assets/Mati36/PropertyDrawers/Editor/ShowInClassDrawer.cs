using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ShowInClassAttribute))]
public class ShowInClassDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ShowInClassAttribute hideInClassAttrib = (ShowInClassAttribute)attribute;
        string objType = property.serializedObject.targetObject.GetType().ToString();

        bool isValid = false;
        foreach (var type in hideInClassAttrib.classShown)
        {
            if (type.ToString() == objType) { isValid = true; break; }
        }

        if (isValid)
            return EditorGUI.GetPropertyHeight(property, label);
        else
            return 0;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ShowInClassAttribute showInClassAttrib = (ShowInClassAttribute)attribute;
        string objType = property.serializedObject.targetObject.GetType().ToString();

        bool isValid = false;
        foreach (var type in showInClassAttrib.classShown)
        {
            if (type.ToString() == objType) { isValid = true; break; }
        }

        if (isValid)
            EditorGUI.PropertyField(position, property, label, true);
    }
}