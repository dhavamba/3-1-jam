﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public float lateralSpeed=5f;
    bool ground = true;
    GameManager gm;
    public bool isGround()
    {
        return ground;
    }



    // Use this for initialization
    void Start()
    {
        gm = GameManager.Instance<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = new Vector3(gm.getCurrentLaneTarget().transform.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime* lateralSpeed);
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        if(ground!=true)
            ground= true;
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        ground = false;
    }

    private void OnTriggerEnter(Collider other)
    {
      GameManager.Instance<GameManager>().ObstacleTriggered(other.gameObject);
    }
}
