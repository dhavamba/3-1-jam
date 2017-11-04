using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    
    public float speed = 4f;
    private float speedFactor = 0.00125f;
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

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        
    }

    private void FixedUpdate()
    {
        //speed += speedFactor;
    }


}
