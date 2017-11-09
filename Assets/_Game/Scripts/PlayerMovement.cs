using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : SimpleMovement
{
    private Transform target;
    private GameManager gm;
    private int Life=3;
    private int Score = 0;
    private int lap=0;

    public int indexPlayer;

    // Use this for initialization
    void Start()
    {
        gm = GameManager.Instance<GameManager>();
    }

    public void LostLife()
    {
        if(Life>0)
            Life -= 1;
    }

    public int getScore()
    {
        return Score;
    }

    public void AddLap()
    {
        lap += 100;
    }

    public void ResetLap()
    {
        lap = 0;
    }
	
	// Update is called once per frame
	protected override void Update ()
    {
        if (!gm.isEndGame(indexPlayer) && Life>0)
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
                    UIInGame.Instance<UIInGame>().RemoveCardMyHand(0,0);
                }
                else if (Input.GetKeyDown("2"))
                {
                    UIInGame.Instance<UIInGame>().RemoveCardMyHand(0,1);
                }
                else if (Input.GetKeyDown("3"))
                {
                    UIInGame.Instance<UIInGame>().RemoveCardMyHand(0,2);
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
                    UIInGame.Instance<UIInGame>().RemoveCardMyHand(1,0);
                }
                else if (Input.GetKeyDown("9"))
                {
                    UIInGame.Instance<UIInGame>().RemoveCardMyHand(1,1);
                }
                else if (Input.GetKeyDown("0"))
                {
                    UIInGame.Instance<UIInGame>().RemoveCardMyHand(1,2);
                }
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            Score = (int)(transform.position.z)+lap;
        }
        
    }
    
    }
