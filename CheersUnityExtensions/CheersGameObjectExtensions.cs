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

    public static T GetComponentInAncestors<T>(this GameObject obj) where T : class => obj.transform.GetComponentInAncestors<T>();

    public static void DestroyAllChildren(this Transform t)
    {
        for (int Idx = 0; Idx < t.childCount; ++Idx)
        {
            Transform child = t.GetChild(Idx);
            child.DestroyGameObject();
        }
        t.DetachChildren();
	}

	public static string FullHierarchyName(this GameObject obj) => obj.transform.FullHierarchyName();

    public static string FullHierarchyName(this Transform tf)
	{
        string result = tf.gameObject.name;
        Transform current = tf.parent;
        while (current != null)
		{
            result = current.gameObject.name + "/" + result;
            current = current.parent;
        }
        return result;
    }

    public static Ray GetRayTo(this Transform from, Transform target) => new Ray(from.position, target.position - from.position);
    public static Ray GetRayTo(this GameObject from, Transform target) => from.transform.GetRayTo(target.transform);

    public static void PlaceConnectingLine(this Transform transform, Vector3 from, Vector3 to, float thickness)
    {
        transform.position = (from + to) * 0.5f;
        transform.rotation = Camera.main.transform.forward.ToLookRotation_ZX(to - from);
        transform.localScale = new Vector3((to - from).magnitude, thickness, 1);
    }

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

    public static void DestroyGameObjectAfterDelay(this MonoBehaviour bhv, float delay)
    {
        bhv.StartCoroutine(DestroyAfterDelay(bhv.gameObject, delay));
    }

    public static IEnumerator DestroyAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        DestroyGameObject(obj);
    }
	
    //=====================================================================================================

    // pick a point within the BoxCollider volume using a normalized position (each axis ranges from -1 to 1, 0 being the collider center)
    public static Vector3 GetNormalizedLocalPosition(this BoxCollider collider, Vector3 normalizedPosition)
    {
        Vector3 offset = collider.size;
        offset.Scale(normalizedPosition * 0.5f);
        return collider.center + offset;
    }

    public static Vector3 GetNormalizedWorldPosition(this BoxCollider collider, Vector3 normalizedPosition)
    {
        return collider.transform.TransformPoint(collider.GetNormalizedLocalPosition(normalizedPosition));
    }


    //=====================================================================================================
    // 16 variants of the same convenience function for instantiating prefabs
    //
    // NB, none of these preserve the prefab's own root transform position/rotation/scale.
    // in my experience it's a bad idea to rely on it.
    //=====================================================================================================

    public static GameObject Instantiate(this GameObject obj, Transform parent, Vector3 localPosition, Quaternion localRotation, Vector3 localScale)
    {
        GameObject result = GameObject.Instantiate(obj, parent);
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
        T result = GameObject.Instantiate(obj, parent);
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