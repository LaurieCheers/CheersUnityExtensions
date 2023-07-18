using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool: ObjectPool<GameObject>
{
    public GameObjectPool(GameObject prefab, Transform parent):
        base(()=>prefab.Instantiate(parent), obj=>obj.SetActive(true), obj=>obj.SetActive(false))
    {
    }
}

public class ObjectPool<T>
{
    System.Func<T> create;
    System.Action<T> activate;
    System.Action<T> deactivate;
    List<T> active = new List<T>();
    Queue<T> inactive = new Queue<T>();

    public ObjectPool(System.Func<T> create, System.Action<T> activate, System.Action<T> deactivate)
    {
        this.create = create;
        this.activate = activate;
        this.deactivate = deactivate;
    }

    public T Get()
    {
        T result;
        if (!inactive.TryDequeue(out result))
        {
            result = create();
        }
        active.Add(result);
        activate(result);
        return result;
    }

    public void Add(T item)
    {
        active.Remove(item);
        inactive.Enqueue(item);
        deactivate(item);
    }

    public void InactivateAll()
    {
        foreach (T item in active)
        {
            deactivate(item);
            inactive.Enqueue(item);
        }

        active.Clear();
    }
}
