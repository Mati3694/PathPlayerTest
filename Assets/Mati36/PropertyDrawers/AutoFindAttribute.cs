using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFindAttribute : PropertyAttribute
{
    public FindMethod searchMethod;

    public AutoFindAttribute(FindMethod searchMethod = FindMethod.OnlyCurrent)
    {
        this.searchMethod = searchMethod;
    }

    public enum FindMethod
    {
        OnlyCurrent,
        SearchInChilds,
        SearchInParent
    }
}
