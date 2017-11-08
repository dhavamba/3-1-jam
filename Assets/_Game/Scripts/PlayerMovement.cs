using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : SimpleMovement
{
    private Transform target;
    private GameManager gm;
    public int indexPlayer;

    // Use this for initialization
    void Start()
    {
        gm = GameManager.Instance<GameManager>();
 
    }

    
	
	// Update is called once per frame
	protected override void Update ()
    {
        
        if (!gm.isEndGame(indexPlayer))
        {
            // DA TOGLIEREEEEEEEEEEEEEEEEEEEEEEEEEEEEE
            if (indexPlayer == 1)
            {

                if (Input.GetKeyDown("d"))
                {

                    gm.ShiftleftLane(indexPlayer);


                }
                else
                if (Input.GetKeyDown(KeyCode.Space))
                {

                    gm.Jump(indexPlayer, JumpIntensity);

                }
                else
                if (Input.GetKeyDown("a"))
                {

                    gm.ShiftRightLane(indexPlayer);


                }
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
           
        }
        
    }
    
    }
