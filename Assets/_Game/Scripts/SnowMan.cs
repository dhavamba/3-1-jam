using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowMan : MonoBehaviour {

    // Use this for initialization
    public GameObject ImpactPrefab;
    public float SlowFactor=2.5f;
    void Start () {
		
	}
	
    public float getSlowFactor()
    {
        return SlowFactor;
    }
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SnowImpact()
    {
        ImpactPrefab.Spawn(new Vector3(transform.position.x,transform.position.y+1f,transform.position.z));
        gameObject.Recycle();
    }
}
