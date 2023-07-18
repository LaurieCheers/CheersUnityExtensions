using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watchable<T>
{
    T _value;
    public T value
    {
        get => _value;
        set
        {
            T oldValue = _value;
            _value = value;
            callbacks.Invoke(oldValue, value);
        }
    }

    RichCallback<T,T> callbacks = new RichCallback<T,T>();

    public Watchable(T initial)
    {
        _value = initial;
    }

    public void AddCallback<T2>(Action<T, T2> callback, T2 userdata, bool shouldCallbackNow = false)
    {
        callbacks.Add(callback, userdata);
        if (shouldCallbackNow)
            callbacks.Invoke(_value, _value);
    }

    public void RemoveCallback<T2>(Action<T, T2> callback)
    {
        callbacks.Remove(callback);
    }

    public void AddCallback(Action<T, T> callback, bool shouldCallbackNow = false)
    {
        callbacks.Add(callback);
        if (shouldCallbackNow)
            callbacks.Invoke(_value, _value);
    }

    public void RemoveCallback(Action<T, T> callback)
    {
        callbacks.Remove(callback);
    }

    public void AddCallback(Action<T> callback, bool shouldCallbackNow = false)
    {
        callbacks.Add(callback);
        if (shouldCallbackNow)
            callbacks.Invoke(_value, _value);
    }

    public void RemoveCallback(Action<T> callback)
    {
        callbacks.Remove(callback);
    }

    public void AddCallback(Action callback, bool shouldCallbackNow = false)
    {
        callbacks.Add(callback);
        if (shouldCallbackNow)
            callbacks.Invoke(_value, _value);
    }

    public void RemoveCallback(Action callback)
    {
        callbacks.Remove(callback);
    }
}
