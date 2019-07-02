using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Struct | AttributeTargets.Class)]
public class ShowIfAttribute : PropertyAttribute
{
    public bool condition;
    public ShowIfAttribute(bool condition)
    {
        this.condition = condition;
    }
}
