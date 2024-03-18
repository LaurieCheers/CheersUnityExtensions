using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CheersSingleton<T> where T : MonoBehaviour
{
    public T target;

    public CheersSingleton(T value)
    {
        this.target = value;
    }

    public static implicit operator T(CheersSingleton<T> instance)
    {
        if (instance.target == null)
            instance.target = GameObject.FindObjectOfType<T>();
        return instance.target;
    }

    public void Clear()
    {
        target = null;
    }
}
