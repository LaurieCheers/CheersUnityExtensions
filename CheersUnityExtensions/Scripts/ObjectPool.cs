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

public class ComponentPool<T> : ObjectPool<T> where T: Component
{
    public ComponentPool(T prefab, Transform parent) :
        base(() => prefab.Instantiate(parent), t => t.gameObject.SetActive(true), t => t.gameObject.SetActive(false))
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

    public ObjectPool(System.Func<T> create, System.Action<T> activate = null, System.Action<T> deactivate = null)
    {
        this.create = create;
        this.activate = activate;
        this.deactivate = deactivate;
    }

    public T GetOrCreate()
    {
        T result;
        if (!inactive.TryDequeue(out result))
        {
            result = create();
        }
        active.Add(result);
        if(activate != null)
            activate(result);
        return result;
    }

    public void Return(T item)
    {
        active.Remove(item);
        inactive.Enqueue(item);
        if(deactivate != null)
            deactivate(item);
    }

    public void InactivateAll()
    {
        if (deactivate != null)
        {
            foreach (T item in active)
            {
                deactivate(item);
                inactive.Enqueue(item);
            }
        }
        else
        {
            foreach (T item in active)
            {
                inactive.Enqueue(item);
            }
        }

        active.Clear();
    }
}
