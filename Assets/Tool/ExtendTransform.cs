using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtendTransform
{
    public static int numberChild(this Transform value)
    {
        Transform parent = value.parent;
        if (parent)
        {
            int i = 0;
            for (i = 0; i < parent.childCount; i++)
            {
                if (parent.GetChild(i).Equals(value))
                {
                    break;
                }
            }
            return i;
        }
        else
        {
            return -1;
        }
    }
}
