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

    public void AddByKey(object key, Action<T> listener)
    {
        callbacks.AddItem(key, listener);
    }

    public void Remove(object key)
    {
        callbacks.Remove(key);
    }

    public void Invoke(T value)
    {
        foreach (List<Action<T>> listeners in callbacks.Values)
            foreach (Action<T> action in listeners)
                action(value);
    }
}

public class RichCallback<T1, T2>
{
    Dictionary<object, List<Action<T1, T2>>> callbacks = new Dictionary<object, List<Action<T1, T2>>>();

    public void Add<T3>(Action<T1, T2, T3> listener, T3 userData)
    {
        callbacks.AddItem(listener, (t1,t2) => listener(t1,t2, userData));
    }

    public void Add<T3>(Action<T2, T3> listener, T3 userData)
    {
        callbacks.AddItem(listener, (t1, t2) => listener(t2, userData));
    }

    public void Add(Action<T1, T2> listener)
    {
        callbacks.AddItem(listener, listener);
    }

    public void Add(Action<T2> listener)
    {
        callbacks.AddItem(listener, (t1, t2) => listener(t2));
    }

    public void Add(Action listener)
    {
        callbacks.AddItem(listener, (t1,t2) => listener());
    }

    public void Remove<T3>(Action<T1, T2, T3> listener)
    {
        callbacks.Remove(listener);
    }

    public void Remove<T3>(Action<T2, T3> listener)
    {
        callbacks.Remove(listener);
    }

    public void Remove(Action<T2> listener)
    {
        callbacks.Remove(listener);
    }

    public void Remove(Action listener)
    {
        callbacks.Remove(listener);
    }

    public void Invoke(T1 value1, T2 value2)
    {
        foreach (List<Action<T1,T2>> listeners in callbacks.Values)
            foreach (Action<T1,T2> action in listeners)
                action(value1, value2);
    }
}
