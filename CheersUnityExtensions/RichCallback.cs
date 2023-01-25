using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RichCallback
{
    Dictionary<object, List<Action>> callbacks = new Dictionary<object, List<Action>>();

    public void AddListener<T>(Action<T> listener, T userData)
    {
        callbacks.AddItem(listener, () => listener(userData));
    }

    public void AddListener(Action listener)
    {
        callbacks.AddItem(listener, listener);
    }

    public void RemoveListener<T>(Action<T> listener)
    {
        callbacks.Remove(listener);
    }

    public void RemoveListener(Action listener)
    {
        callbacks.Remove(listener);
    }

    public void Invoke()
    {
        foreach (List<Action> listeners in callbacks.Values)
            foreach (Action action in listeners)
                action();
    }
}

public class RichCallback<T>
{
    Dictionary<object, List<Action<T>>> callbacks = new Dictionary<object, List<Action<T>>>();

    public void Add<T2>(Action<T, T2> listener, T2 userData)
    {
        callbacks.AddItem(listener, t => listener(t, userData));
    }

    public void Add(Action<T> listener)
    {
        callbacks.AddItem(listener, listener);
    }

    public void Add(Action listener)
    {
        callbacks.AddItem(listener, t=> listener());
    }

    public void Remove<T2>(Action<T, T2> listener)
    {
        callbacks.Remove(listener);
    }

    public void Remove(Action<T> listener)
    {
        callbacks.Remove(listener);
    }

    public void Remove(Action listener)
    {
        callbacks.Remove(listener);
    }

    public void Invoke(T value)
    {
        foreach (List<Action<T>> listeners in callbacks.Values)
            foreach (Action<T> action in listeners)
                action(value);
    }
}
