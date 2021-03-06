﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour {

    Rigidbody rb;
    public float intensityForce;
    public Transform cameraTransform;
    public GameObject ImpactPrefab;
    public float SlowFactor = 2.5f;
    // Use this for initialization
   
    public void Send(Vector3 direction)
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(direction * intensityForce, ForceMode.Impulse);
    }


	// Update is called once per frame
	void Update ()
    {
        if (cameraTransform != null)
        {
            if (cameraTransform.transform.position.z > transform.position.z)
                Destroy(gameObject);
        }
	}

    public void SetCamera(Transform t)
    {
        cameraTransform = t;

    }
    public void SnowImpact()
    {
        ImpactPrefab.Spawn(new Vector3(transform.position.x, transform.position.y, transform.position.z));
        gameObject.Recycle();
    }

    public float getSlowFactor()
    {
        return SlowFactor;
    }
}
