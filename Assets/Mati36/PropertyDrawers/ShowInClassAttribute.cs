using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Struct | AttributeTargets.Class)]
public class ShowInClassAttribute : PropertyAttribute
{
    public Type[] classShown;
    public ShowInClassAttribute(params Type[] classShown)
    {
        this.classShown = classShown;
    }
}