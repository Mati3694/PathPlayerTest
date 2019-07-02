using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

[CustomPropertyDrawer(typeof(AutoFindAttribute))]
public class AutoFindDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.objectReferenceValue == null)
        {
            EditorGUI.PrefixLabel(position, new GUIContent(label.text + " [AUTO]"));

            Rect fieldRect = new Rect(position);
            fieldRect.width -= EditorGUIUtility.labelWidth;
            fieldRect.x += EditorGUIUtility.labelWidth;

            AutoFindAttribute propAttrib = (AutoFindAttribute)attribute;

            var inspectedObj = property.serializedObject.targetObject;

            if (inspectedObj is Component comp)
            {
                Component component = null;
                if (propAttrib.searchMethod == AutoFindAttribute.FindMethod.OnlyCurrent)
                    component = comp.GetComponent(fieldInfo.FieldType);
                else if (propAttrib.searchMethod == AutoFindAttribute.FindMethod.SearchInChilds)
                    component = comp.GetComponentInChildren(fieldInfo.FieldType);
                else if (propAttrib.searchMethod == AutoFindAttribute.FindMethod.SearchInParent)
                    component = comp.GetComponentInParent(fieldInfo.FieldType);
                if (component != null)
                {
                    property.objectReferenceValue = component;
                    property.serializedObject.ApplyModifiedProperties();
                    property.serializedObject.Update();
                }
                else
                    GUI.Label(fieldRect, "Not found");
            }
            else
            {
                GUI.Label(fieldRect, "Field is not component");
            }

        }
        else
            EditorGUI.PropertyField(position, property, new GUIContent(label.text + " [AUTO]"));

    }
}
