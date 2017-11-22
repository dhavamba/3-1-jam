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
    private Dictionary<string, bool> coolDownKey;
    public int indexPlayer;
    bool win=false;

    // Use this for initialization
    void Start()
    {
        gm = GameManager.Instance<GameManager>();
        coolDownKey = new Dictionary<string, bool>();

        coolDownKey.Add("1", true);
        coolDownKey.Add("2", true);
        coolDownKey.Add("3", true);

        coolDownKey.Add("8", true);
        coolDownKey.Add("9", true);
        coolDownKey.Add("0", true);

    }
    public void setWinner(bool w)
    {
        win = w;
    }


    public void LostLife()
    {
        if(Life>0)
            Life -= 1;
    }


    public int GetLife()
    {
        return Life;
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
        if (lap > 100)
            lap = lap - 100;
        else
            lap = 0;
    }
	
	// Update is called once per frame
	protected override void Update ()
    {
            if (!gm.isEndGame(indexPlayer) && Life > 0 && !win)
            {
                // DA TOGLIEREEEEEEEEEEEEEEEEEEEEEEEEEEEEE
                if (indexPlayer == 0)
                {
                    if (Input.GetKeyDown("d"))
                    {
                        gm.ShiftleftLane(indexPlayer);
                    }
                    else if (Input.GetKeyDown("s"))
                    {
                        gm.Jump(indexPlayer, JumpIntensity);
                    }
                    else if (Input.GetKeyDown("a"))
                    {
                        gm.ShiftRightLane(indexPlayer);
                    }
                    else if (Input.GetKeyDown("1") && coolDownKey["1"])
                    {
                        UIInGame.Instance<UIInGame>().RemoveCardMyHand(0, 0);
                        StartCoroutine(waitKey("1"));
                    }

                    else if (Input.GetKeyDown("2") && coolDownKey["2"])
                    {
                        UIInGame.Instance<UIInGame>().RemoveCardMyHand(0, 1);
                        StartCoroutine(waitKey("2"));
                    }
                    else if (Input.GetKeyDown("3") && coolDownKey["3"])
                    {
                        UIInGame.Instance<UIInGame>().RemoveCardMyHand(0, 2);
                        StartCoroutine(waitKey("3"));
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
                    else if (Input.GetKeyDown("8") && coolDownKey["8"])
                    {
                        UIInGame.Instance<UIInGame>().RemoveCardMyHand(1, 0);
                        StartCoroutine(waitKey("8"));
                    }
                    else if (Input.GetKeyDown("9") && coolDownKey["9"])
                    {
                        UIInGame.Instance<UIInGame>().RemoveCardMyHand(1, 1);
                        StartCoroutine(waitKey("9"));
                    }
                    else if (Input.GetKeyDown("0") && coolDownKey["0"])
                    {
                        UIInGame.Instance<UIInGame>().RemoveCardMyHand(1, 2);
                        StartCoroutine(waitKey("0"));
                    }
                }
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                Score = (int)(transform.position.z) + lap;
            }
        
    }
    
    IEnumerator waitKey(string key)
    {
        coolDownKey[key] = false;
        yield return new WaitForSeconds(0.4f);
        coolDownKey[key] = true;

    }


    }
