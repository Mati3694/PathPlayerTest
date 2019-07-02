using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class RangeMinMaxAttribute : PropertyAttribute
{
    public float sliderMin, sliderMax;
    public RangeMinMaxAttribute(float sliderMin, float sliderMax)
    {
        this.sliderMin = sliderMin; this.sliderMax = sliderMax;
    }
}