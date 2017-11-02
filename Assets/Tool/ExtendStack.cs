using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendStack<T> : Stack<T>
{
    public bool IsEmpty()
    {
        return Count == 0;
    }
}
