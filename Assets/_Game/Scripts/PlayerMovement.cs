using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    
    public float speed = 4f;
    public float JumpIntensity=300f;
    private float speedFactor = 0.00125f;
    private Vector3 startPosition;
    Transform target;
    GameManager gm;

    // Use this for initialization
    void Start()
    {
        gm = GameManager.Instance<GameManager>();
        target = gm.getCurrentLaneTarget();
        startPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!gm.isEndGame())
        {

            if (Input.GetKeyDown("d"))
            {

                gm.ShiftleftLane();
                target = gm.getCurrentLaneTarget();

            }

            if (Input.GetKeyDown(KeyCode.Space))
            {

                gm.Jump(JumpIntensity);

            }

            if (Input.GetKeyDown("a"))
            {

                gm.ShiftRightLane();
                target = gm.getCurrentLaneTarget();

            }

            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        
    }

 

    public void ResetTostart()
    {
        speed += speedFactor;
        transform.position = startPosition;

    }

    


}
