using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : SimpleMovement
{
    private Transform target;
    private GameManager gm;

    // Use this for initialization
    void Start()
    {
        gm = GameManager.Instance<GameManager>();
        target = gm.getCurrentLaneTarget();
    }
	
	// Update is called once per frame
	protected override void Update ()
    {
        if (!gm.isEndGame())
        {

            if (Input.GetKeyDown("d"))
            {

                gm.ShiftleftLane();
                target = gm.getCurrentLaneTarget();

            }
            else
            if (Input.GetKeyDown(KeyCode.Space))
            {

                gm.Jump(JumpIntensity);

            }
            else
            if (Input.GetKeyDown("a"))
            {

                gm.ShiftRightLane();
                target = gm.getCurrentLaneTarget();

            }
            else
            {
                Translate();
            }
        }  
    }
}
