using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class CustomLabelAttribute : PropertyAttribute
{
    public string labelName;
    public CustomLabelAttribute(string labelName)
    {
        this.labelName = labelName;
    }
}
