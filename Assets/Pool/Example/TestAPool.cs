using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAPool : MonoBehaviour
{
    public GameObject obj;

	// Use this for initialization
	void Start ()
    {
        Create();
	}

    void Create()
    {
        StaticPool.Instantiate(obj, Vector3.down, gameObject.transform, 2);
        Invoke("Create", 1f);
    }
}
