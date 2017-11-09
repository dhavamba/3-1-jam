using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public Transform[] Lanes;

    private  Transform[] CurrentLanes;

    // 0 è LA LISTA DEGLI OGGETTI DA DROPPARE SUL GIOCATORE 0(INVIATI DAL GIOCATORE 1)
    // 1 è LA LISTA DEGLI OGGETTI DA DROPPARE SUL GIOCATORE 1(INVIATI DAL GIOCATORE 0)

    private List<List<ValueCard>> ObstacleList;
    private int [] indexLanes;
    private bool[] dropOtherObstacles;

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
        ObstacleList = new List< List<ValueCard> >();
        dropOtherObstacles = new bool[Player.Length];
        for (int i = 0; i < Player.Length; i++)
        {
            dropOtherObstacles[i] = true;
        }
        for (int i = 0; i < Player.Length; i++)
        {
            ObstacleList.Add(new List<ValueCard>());
        }

        for (int i = 0; i < indexLanes.Length; i++)
        {
            indexLanes[i] = 1;
        }

        for (int i = 0; i < indexLanes.Length; i++)
        {
            indexLanes[i] = 1;
        }
        for (int i = 0; i < Player.Length; i++)
        {
            endGame[i] = false;
        }
        for (int i = 0; i < Player.Length; i++)
        {
            CurrentLanes[i] = Lanes[i].GetChild(indexLanes[i]);
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
            if (Vector3.Distance(Player[i].transform.position, Target[i].position)>20f && dropOtherObstacles[i])
                {
                    if (ObstacleList[i].Count > 0)
                    {

                        DropObstacle(i, ObstacleList[i][0]);
                        ObstacleList[i].Remove(ObstacleList[i][0]);
                        dropOtherObstacles[i] = false;
                    }
                }
        }


    }

        void ResetPlayerPos(int index)
        {
            PlayerPivot[index].GetComponent<SimpleMovement>().ResetTostart();
            dropOtherObstacles[index] = true;

        }

          IEnumerator StartGame(int index)
        {
            yield return new WaitForSeconds(1.5f);
            PlayerPivot[index].GetComponent<SimpleMovement>().StartGame();
            endGame[index] = false;
        }

        public void AddDropObstacle(int indexPlayer, ValueCard ObstacleValue)
        {
            if(indexPlayer==0)
                 ObstacleList[1].Add(ObstacleValue);
            else
                ObstacleList[0].Add(ObstacleValue);

        }

    public void DropObstacle(int indexPlayer, ValueCard ObstacleValue)
        {
            GameObject obstacle;
            switch (ObstacleValue)
            {
            case ValueCard.BouldersBelow:
               
                obstacle= StaticPool.Instantiate(obManager.getDropObstacle(ObstacleValue), new Vector3(Player[indexPlayer].transform.position.x, Player[indexPlayer].transform.position.y, Player[indexPlayer].transform.position.z + 15f));
                obstacle.GetComponent<SnowBall>().SetCamera(PlayerPivot[indexPlayer].transform.GetChild(1).transform);
                obstacle.GetComponent<SnowBall>().Send(-Vector3.forward);
                    break;
            /*
            case ValueCard.BouldersAbove:
               
                obstacle = StaticPool.Instantiate(obManager.getDropObstacle(ObstacleValue), new Vector3(Player[indexPlayer].transform.position.x, Player[indexPlayer].transform.position.y, Player[indexPlayer].transform.position.z - 15f));          
                obstacle.GetComponent<SnowBall>().SetCamera(PlayerPivot[indexPlayer].transform.GetChild(1).transform);
                obstacle.GetComponent<SnowBall>().Send(-Vector3.forward);
                break;
            */

            case ValueCard.Freze:
                StartCoroutine(Frost(indexPlayer));
                break;

            case ValueCard.SpeedUp:
                StartCoroutine(SpeedUp(indexPlayer,3.5f));
                break;

            case ValueCard.Pillars:
                obstacle = StaticPool.Instantiate(obManager.getDropObstacle(ObstacleValue), new Vector3(Player[indexPlayer].transform.position.x, Player[indexPlayer].transform.position.y, Player[indexPlayer].transform.position.z + 15f));
                break;
            default:
                Debug.Log(ObstacleValue);
               
                break;

        }
        }

    IEnumerator Frost(int indexPlayer)
    {
        PlayerPivot[indexPlayer].transform.GetChild(1).GetComponent<FrostEffect>().FrostAmount = 0.5f;
        PlayerPivot[indexPlayer].GetComponent<PlayerMovement>().speed -= 2.5f;
        yield return new WaitForSeconds(4.5f);
        PlayerPivot[indexPlayer].transform.GetChild(1).GetComponent<FrostEffect>().FrostAmount = 0f;
        PlayerPivot[indexPlayer].GetComponent<PlayerMovement>().speed += 2.5f;
    }

    IEnumerator SpeedUp(int indexPlayer,float SpeedTime)
    {
        
        PlayerPivot[indexPlayer].GetComponent<PlayerMovement>().speed += 2.5f;
        yield return new WaitForSeconds(SpeedTime);
        PlayerPivot[indexPlayer].GetComponent<PlayerMovement>().speed -= 2.5f;
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

            case "BouldersBelow":
                collider.GetComponent<SnowBall>().SnowImpact();
                PlayerPivot[playerIndex].GetComponent<PlayerMovement>().Slow(collider.GetComponent<SnowBall>().getSlowFactor());
                break;

            case "BouldersAbove":
                collider.GetComponent<SnowBall>().SnowImpact();
                PlayerPivot[playerIndex].GetComponent<PlayerMovement>().Slow(collider.GetComponent<SnowBall>().getSlowFactor());
                break;

            case "Pillars":
                endGame[playerIndex] = true;
                StartCoroutine(StartGame(playerIndex));
                break;
        }
        
        }

    public float getPlayerSpeed(int index)
    {
        return PlayerPivot[index].GetComponent<PlayerMovement>().speed;
    }
            
        }

