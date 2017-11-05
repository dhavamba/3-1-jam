using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{

    bool ground = true;
    public bool isGround()
    {
        return ground;
    }



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionStay(Collision collisionInfo)
    {
        ground= true;
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        ground = false;
    }
}
