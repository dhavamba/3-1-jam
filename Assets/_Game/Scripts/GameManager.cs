using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public Transform[] Lanes;

    private  Transform[] CurrentLanes;
    
    private int [] indexLanes;
    

    public GameObject [] PlayerPivot;
    public GameObject [] Player;
    
    ObstaclesManager obManager;
    private bool [] endGame;


    public Transform [] Target;

	void Start ()
    {
        obManager = GetComponent<ObstaclesManager>();


    }

    
    private void Awake()
    {
        indexLanes = new int[Player.Length];
        CurrentLanes = new Transform[Player.Length];
        endGame = new bool[Player.Length];

        for (int i = 0; i < indexLanes.Length; i++)
        {
            indexLanes[i] = 1;
        }

        for (int i = 0; i < Player.Length; i++)
        {
           CurrentLanes[i] = Lanes[i].GetChild(indexLanes[i]);
        }
        for (int i = 0; i < Player.Length; i++)
        {
            endGame[i] = false;
        }

    }

    public bool isEndGame(int playerIndex)
    {
        return endGame[playerIndex];
    }

    public void ShiftRightLane(int playerIndex)
    {
        
        if (indexLanes[playerIndex] > 0)
        {
            indexLanes[playerIndex]--;
            CurrentLanes[playerIndex] = Lanes[playerIndex].GetChild(indexLanes[playerIndex]);
            Player[playerIndex].transform.Rotate(0, 0, 0);
        }
        
    }

   

    public void ShiftleftLane(int playerIndex)
    {
        
        if (indexLanes[playerIndex] < Lanes[playerIndex].childCount-1 )
        {
            indexLanes[playerIndex]++;
            CurrentLanes[playerIndex] = Lanes[playerIndex].GetChild(indexLanes[playerIndex]);
            Player[playerIndex].transform.Rotate(0, 0, 0);
        }
        
    }

    public void Jump(int playerIndex,float intensity)
    {
        
        if (Player[playerIndex].GetComponent<PlayerCollider>().isGround())
        {
            Player[playerIndex].GetComponent<Rigidbody>().AddForce(Vector3.up * intensity, ForceMode.Impulse);
            Player[playerIndex].transform.Rotate(0, 0, 0);
        }
        

    }

    public Transform getCurrentLaneTarget(int playerIndex)
    {
        return CurrentLanes[playerIndex].transform;
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Player.Length; i++)
        {
            if (Vector3.Distance(Player[i].transform.position, Target[i].position) < 2f)
            {
                Debug.Log("Reset");
                obManager.ResetObstaclesArea();
                ResetPlayerPos(i);
                obManager.SpawObject();
            }


        }
    }

        void ResetPlayerPos(int index)
        {
            PlayerPivot[index].GetComponent<SimpleMovement>().ResetTostart();

        }

          IEnumerator StartGame(int index)
        {
            yield return new WaitForSeconds(1.5f);
            PlayerPivot[index].GetComponent<SimpleMovement>().StartGame();
            endGame[index] = false;
        }


        public void ObstacleTriggered(int playerIndex,GameObject collider)
        {
            
        switch(collider.ReturnTags()[0])
        {
            case "Truck":
                endGame[playerIndex] = true;
                StartCoroutine(StartGame(playerIndex));
                break;

            case "SnowMan":
                collider.GetComponent<SnowMan>().SnowImpact();
                PlayerPivot[playerIndex].GetComponent<PlayerMovement>().Slow(collider.GetComponent<SnowMan>().getSlowFactor());
                break;
        }
        
        }
            
        }

