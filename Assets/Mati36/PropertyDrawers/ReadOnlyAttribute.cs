using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct)]
public class ReadOnlyAttribute : PropertyAttribute
{
    public bool onlyInPlay;

    public ReadOnlyAttribute()
    {
        this.onlyInPlay = false;
    }

    public ReadOnlyAttribute(bool onlyInPlay)
    {
        this.onlyInPlay = onlyInPlay;
    }
}
