using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBPool : MonoBehaviour {

    private void OnCollisionEnter(Collision coll)
    {
        StaticPool.Destroy(gameObject);
    }
}
