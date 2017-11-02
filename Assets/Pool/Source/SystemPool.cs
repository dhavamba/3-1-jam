using System.Collections.Generic;
using UnityEngine;

public class SystemPool : MonoBehaviour
{
    public float max;
    public GameObject Prefab { private get; set; }

    private List<GameObject> listDisable;
    private List<GameObject> listAble;
    private GameObject aux;

    private void Awake()
    {
        listDisable = new List<GameObject>();
        listAble = new List<GameObject>();
    }

    private void OnDestroy()
    {
        StaticPool.RemovePool(gameObject);
    }

    /// <summary>
    /// Disable GameObject
    /// </summary>
    public void Disable(GameObject obj)
    {
        obj.SetActive(false);
        listDisable.Add(obj);
        listAble.Remove(obj);
    }

    /// <summary>
    /// Disable all GameObject
    /// </summary>
    public void AllDisable()
    {
        foreach (GameObject obj in listAble)
        {
            obj.SetActive(false);
            listDisable.Add(obj);
        }
        listAble.Clear();
    }

    /// <summary>
    /// Create or Recycle GameObject
    /// </summary>
    public GameObject InstantiateObject(Transform parent, Vector3 position)
    {
        if (parent != null)
        {
            position = parent.position + position;
        }

        if (listDisable.Count == 0)
        {
            if (listAble.Count >= max)
            {
                Disable(listAble[0]);
                aux = _RecycleObject();
            }
            else
            {
                aux = _IstantiateNewObject(position);
            }
        }
        else
        {
            aux = _RecycleObject();
        }

        _ResetElement(position);
        return aux;
    }

    private GameObject _RecycleObject()
    {
        aux = listDisable[0];
        aux.SetActive(true);
        listDisable.Remove(aux);
        listAble.Add(aux);
        return aux;
    }

    private void _ResetElement(Vector3 position)
    {
        aux.transform.position = position;
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        transform.rotation = Quaternion.identity;
    }

    private GameObject _IstantiateNewObject(Vector3 position)
    {
        aux = GameObject.Instantiate(Prefab, position, Quaternion.identity, transform);
        listAble.Add(aux);
        return aux;
    }

}

