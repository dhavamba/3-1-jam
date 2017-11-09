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
            if (indexPlayer == 0)
            {
                if (Input.GetKeyDown("d"))
                {
                    gm.ShiftleftLane(indexPlayer);
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    gm.Jump(indexPlayer, JumpIntensity);
                }
                else if (Input.GetKeyDown("a"))
                {
                    gm.ShiftRightLane(indexPlayer);
                }
                else if (Input.GetKeyDown("1"))
                {
                    UIInGame.Instance<UIInGame>().RemoveCardMyHand("00");
                }
                else if (Input.GetKeyDown("2"))
                {
                    UIInGame.Instance<UIInGame>().RemoveCardMyHand("01");
                }
                else if (Input.GetKeyDown("3"))
                {
                    UIInGame.Instance<UIInGame>().RemoveCardMyHand("02");
                }
            }
            else
            {
                if (Input.GetKeyDown("l"))
                {
                    gm.ShiftleftLane(indexPlayer);
                }
                else if (Input.GetKeyDown("k"))
                {
                    gm.Jump(indexPlayer, JumpIntensity);
                }
                else if (Input.GetKeyDown("j"))
                {
                    gm.ShiftRightLane(indexPlayer);
                }
                else if (Input.GetKeyDown("8"))
                {
                    UIInGame.Instance<UIInGame>().RemoveCardMyHand("10");
                }
                else if (Input.GetKeyDown("9"))
                {
                    UIInGame.Instance<UIInGame>().RemoveCardMyHand("11");
                }
                else if (Input.GetKeyDown("0"))
                {
                    UIInGame.Instance<UIInGame>().RemoveCardMyHand("12");
                }
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
           
        }
        
    }
    
    }
