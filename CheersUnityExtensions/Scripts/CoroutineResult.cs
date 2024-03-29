using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineResult<T>
{
    public T value;

    public CoroutineResult(T initialValue = default)
    {
        this.value = initialValue;
    }
}
