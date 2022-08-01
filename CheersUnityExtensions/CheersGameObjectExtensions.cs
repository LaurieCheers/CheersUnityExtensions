using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CheersGameObjectExtensions
{
    // Like TransformPoint, InverseTransformPoint etc but for rotation
    public static Quaternion TransformRotation(this Transform self, Quaternion localRotation) => self.rotation * localRotation;
    public static Quaternion InverseTransformRotation(this Transform self, Quaternion worldRotation) => Quaternion.Inverse(self.rotation) * worldRotation;

    // Seems weird that this isn't predefined
    public static IEnumerable<Transform> GetChildren(this Transform parent)
    {
        for (int Idx = 0; Idx < parent.childCount; ++Idx)
        {
            yield return parent.GetChild(Idx);
        }
    }

    // Search upward through the transform hierarchy and return the first T we find.
    public static T GetComponentInAncestors<T>(this Transform obj) where T : class
    {
        while (obj != null)
        {
            T result = obj.GetComponent<T>();
            if (result != null)
                return result;
            obj = obj.parent;
        }
        return null;
    }

    public static T GetComponentInAncestors<T>(this Component obj) where T : class => obj.transform.GetComponentInAncestors<T>();
    public static T GetComponentInAncestors<T>(this GameObject obj) where T : class => obj.transform.GetComponentInAncestors<T>();

    //=====================================================================================================
    // GameObjects and Components both have a Destroy() function, meaning that it's easy to accidentally
    // delete a component when you meant to delete the GameObject it's on. Explicit is better than implicit.
    //=====================================================================================================
    public static void DestroyGameObject(this GameObject obj)
    {
        GameObject.Destroy(obj);
    }

    public static void DestroyGameObject(this Component cmp)
    {
        if (cmp != null)
            GameObject.Destroy(cmp.gameObject);
    }

    //=====================================================================================================
    // 16 variants of the same convenience function for instantiating prefabs
    //
    // NB, none of these preserve the prefab's own root transform position/rotation/scale.
    // in my experience it's a bad idea to rely on it.
    //=====================================================================================================

    public static GameObject Instantiate(this GameObject obj, Transform parent, Vector3 localPosition, Quaternion localRotation, Vector3 localScale)
    {
        GameObject result = GameObject.Instantiate(obj, parent.transform);
        result.transform.localPosition = localPosition;
        result.transform.localRotation = localRotation;
        result.transform.localScale = localScale;
        return result;
    }

    public static GameObject Instantiate(this GameObject obj, Transform parent, Vector3 localPosition, Quaternion localRotation) => obj.Instantiate(parent, localPosition, localRotation, Vector3.one);
    public static GameObject Instantiate(this GameObject obj, Transform parent, Vector3 localPosition) => obj.Instantiate(parent, localPosition, Quaternion.identity, Vector3.one);
    public static GameObject Instantiate(this GameObject obj, Transform parent) => obj.Instantiate(parent, Vector3.zero, Quaternion.identity, Vector3.one);

    public static GameObject Instantiate(this GameObject obj, Vector3 worldPosition, Quaternion worldRotation, Vector3 localScale)
    {
        GameObject result = GameObject.Instantiate(obj);
        result.transform.position = worldPosition;
        result.transform.rotation = worldRotation;
        result.transform.localScale = localScale;
        return result;
    }

    public static GameObject Instantiate(this GameObject obj, Vector3 worldPosition, Quaternion worldRotation) => obj.Instantiate(worldPosition, worldRotation, Vector3.one);
    public static GameObject Instantiate(this GameObject obj, Vector3 worldPosition) => obj.Instantiate(worldPosition, Quaternion.identity, Vector3.one);
    public static GameObject Instantiate(this GameObject obj) => obj.Instantiate(Vector3.zero, Quaternion.identity, Vector3.one);


    public static T Instantiate<T>(this T obj, Transform parent, Vector3 localPosition, Quaternion localRotation, Vector3 localScale) where T : Component
    {
        T result = GameObject.Instantiate(obj, parent.transform);
        result.transform.localPosition = localPosition;
        result.transform.localRotation = localRotation;
        result.transform.localScale = localScale;
        return result;
    }

    public static T Instantiate<T>(this T obj, Transform parent, Vector3 localPosition, Quaternion localRotation) where T : Component => obj.Instantiate(parent, localPosition, localRotation, Vector3.one);
    public static T Instantiate<T>(this T obj, Transform parent, Vector3 localPosition) where T : Component => obj.Instantiate(parent, localPosition, Quaternion.identity, Vector3.one);
    public static T Instantiate<T>(this T obj, Transform parent) where T : Component => obj.Instantiate(parent, Vector3.zero, Quaternion.identity, Vector3.one);

    public static T Instantiate<T>(this T obj, Vector3 worldPosition, Quaternion worldRotation, Vector3 localScale) where T : Component
    {
        T result = GameObject.Instantiate(obj);
        result.transform.position = worldPosition;
        result.transform.rotation = worldRotation;
        result.transform.localScale = localScale;
        return result;
    }

    public static T Instantiate<T>(this T obj, Vector3 worldPosition, Quaternion worldRotation) where T : Component => obj.Instantiate(worldPosition, worldRotation, Vector3.one);
    public static T Instantiate<T>(this T obj, Vector3 worldPosition) where T : Component => obj.Instantiate(worldPosition, Quaternion.identity, Vector3.one);
    public static T Instantiate<T>(this T obj) where T : Component => obj.Instantiate(Vector3.zero, Quaternion.identity, Vector3.one);
}