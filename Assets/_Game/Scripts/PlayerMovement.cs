using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    
    public float speed = 4f;
    Transform target;

    // Use this for initialization
    void Start()
    {
        target = GameManager.instance.getCurrentLaneTarget();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown("d"))
        {

            GameManager.instance.ShiftleftLane();
            target = GameManager.instance.getCurrentLaneTarget();

        }

        if (Input.GetKeyDown("a"))
        {

            GameManager.instance.ShiftRightLane();
            target = GameManager.instance.getCurrentLaneTarget();

        }

        transform.position= Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        speed += 0.001f;
        
    }


}
