using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(RangeMinMaxAttribute))]
public class RangeMinMaxDrawer : PropertyDrawer {

    float minValue, maxValue;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return (base.GetPropertyHeight(property, label) * 2f);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        RangeMinMaxAttribute info = (RangeMinMaxAttribute)attribute;
        EditorGUI.PrefixLabel(position, label);
        Rect sliderPos = position;
        sliderPos.x += EditorGUIUtility.labelWidth;
        sliderPos.width -= EditorGUIUtility.labelWidth;
        sliderPos.height = EditorGUIUtility.singleLineHeight;
        if(property.type != "Vector2") { GUI.Label(sliderPos, "Only Vector2 Supported"); return; }

        Vector2 vectorValue = property.vector2Value;
        minValue = vectorValue.x;
        maxValue = vectorValue.y;
        if (minValue < info.sliderMin )
            minValue = info.sliderMin;
        if (maxValue < info.sliderMin)
            maxValue = info.sliderMin;


        EditorGUI.BeginChangeCheck();
        EditorGUI.MinMaxSlider(sliderPos, ref minValue, ref maxValue, info.sliderMin, info.sliderMax);
        if(EditorGUI.EndChangeCheck())
        {
            vectorValue.x = minValue;
            vectorValue.y = maxValue;
            property.vector2Value = vectorValue;
        }

        Rect minPos = sliderPos;
        minPos.y += EditorGUIUtility.singleLineHeight;
        minPos.width *= 0.5f;
        minPos.height = EditorGUIUtility.singleLineHeight;
        EditorGUI.BeginChangeCheck();
        minValue = EditorGUI.FloatField(minPos, minValue);
        if(EditorGUI.EndChangeCheck())
        {
            vectorValue.x = Mathf.Max(minValue, info.sliderMin);
            vectorValue.x = Mathf.Min(vectorValue.x, vectorValue.y);
            property.vector2Value = vectorValue;
        }

        Rect maxPos = sliderPos;
        maxPos.x += minPos.width;
        maxPos.y += EditorGUIUtility.singleLineHeight;
        maxPos.width *= 0.5f;
        maxPos.height = EditorGUIUtility.singleLineHeight;
        EditorGUI.BeginChangeCheck();
        maxValue = EditorGUI.FloatField(maxPos, maxValue);
        if (EditorGUI.EndChangeCheck())
        {
            vectorValue.y = Mathf.Min(maxValue, info.sliderMax);
            vectorValue.y = Mathf.Max(vectorValue.y, vectorValue.x);
            property.vector2Value = vectorValue;
        }
    }
}
