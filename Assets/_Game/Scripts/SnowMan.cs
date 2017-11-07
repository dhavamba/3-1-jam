using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowMan : MonoBehaviour {

    // Use this for initialization
    public GameObject ImpactPrefab;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SnowImpact()
    {
        StaticPool.Instantiate(ImpactPrefab, new Vector3(transform.position.x,transform.position.y+1f,transform.position.z));
        StaticPool.Destroy(gameObject);
    }
}
