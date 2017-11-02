using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtendGameObject
{
    public static GameObject Instantiate(string name, Transform parent = null, GameObject prefab = null, bool pool = false, float max = Mathf.Infinity)
    {
        return Instantiate(name, Vector3.zero, Quaternion.identity, parent, prefab, pool, max);
    }

    public static GameObject Instantiate(string name, Vector3 position, Transform parent = null, GameObject prefab = null, bool pool = false, float max = Mathf.Infinity)
    {
        return Instantiate(name, position, Quaternion.identity, parent, prefab, pool, max);
    }

    public static GameObject Instantiate(Transform parent = null, GameObject prefab = null, bool pool = false, float max = Mathf.Infinity)
    {
        return Instantiate("GameObject", Vector3.zero, Quaternion.identity, parent, prefab, pool, max);
    }

    public static GameObject Instantiate(Vector3 position, Transform parent = null, GameObject prefab = null, bool pool = false, float max = Mathf.Infinity)
    {
        return Instantiate("GameObject", position, Quaternion.identity, parent, prefab, pool, max);
    }

    public static GameObject Instantiate(string name, Vector3 position, Quaternion quat, Transform parent = null, GameObject prefab = null, bool pool = false, float max = Mathf.Infinity)
    {
        GameObject aux;
        if (!pool)
        {
            aux = GameObject.Instantiate(prefab, position, quat, parent);
            aux.name = name;
        }
        else
        {
            aux = StaticPool.Instantiate(prefab, position, parent, max);
            aux.AddOnlyOneComponent<MultiTags>();
            aux.AddTags("pool");
        }
        return aux;
    }

    public static void Destroy(GameObject obj)
    {
        if (obj.HaveTags("pool"))
        {
            StaticPool.Destroy(obj);
        }
        else
        {
            GameObject.Destroy(obj);
        }
    }

    public static T[] FindOtherObjectsOfType<T>(this GameObject obj) where T : MonoBehaviour
    {
        T[] names = GameObject.FindObjectsOfType<T>();
        List<T> list = new List<T>(names);
        list.Remove(list.Find(x => x.gameObject.GetInstanceID() == obj.GetInstanceID()));
        return list.ToArray();
    }

    public static T AddOnlyOneComponent<T>(this GameObject obj) where T : Component
    {
        if (!obj.GetComponent<T>())
        {
            obj.AddComponent<T>();
        }
        return obj.GetComponent<T>();
    }
}
