using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    bool End=false;


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
        if(!End)
        { 
        for (int i = 0; i < Player.Length; i++)
        {
            if (Vector3.Distance(Player[i].transform.position, Target[i].position) < 2f)
            {
                Debug.Log("Reset");
                obManager.ResetObstaclesArea(i);
                PlayerPivot[i].GetComponent<PlayerMovement>().AddLap();
                ResetPlayerPos(i);
                obManager.SpawObject(i);
            }
            if (Vector3.Distance(Player[i].transform.position, Target[i].position) > 20f && dropOtherObstacles[i])
            {
                if (ObstacleList[i].Count > 0)
                {
                    StartCoroutine(WaitToDropObstacle(i));
                }
            }


        }

            if (PlayerPivot[0].GetComponent<PlayerMovement>().GetLife() == 0 || PlayerPivot[1].GetComponent<PlayerMovement>().GetLife() == 0)
            {
                if (PlayerPivot[1].GetComponent<PlayerMovement>().getScore() < PlayerPivot[0].GetComponent<PlayerMovement>().getScore())
                {
                    PlayerPivot[0].GetComponent<PlayerMovement>().setWinner(true);
                    UIInGame.Instance<UIInGame>().setWinner(0, 1);
                    End = true;
                    StartCoroutine(ResetGame());


                }
                else
                {
                    PlayerPivot[1].GetComponent<PlayerMovement>().setWinner(true);
                    UIInGame.Instance<UIInGame>().setWinner(1, 0);
                    End = true;
                    StartCoroutine(ResetGame());

                }
            }

        }
    

    }
    IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);

    }


    IEnumerator WaitToDropObstacle(int i)
    {
        DropObstacle(i, ObstacleList[i][0]);
        ObstacleList[i].Remove(ObstacleList[i][0]);
        dropOtherObstacles[i] = false;   
        yield return new WaitForSeconds(2.5f);
        dropOtherObstacles[i] = true;

    }


        void ResetPlayerPos(int index)
        {
            PlayerPivot[index].GetComponent<SimpleMovement>().ResetTostart();
        }

          IEnumerator StartGame(int index)
        {
            PlayerPivot[index].GetComponent<PlayerMovement>().ResetLap();
            yield return new WaitForSeconds(1.5f);
            PlayerPivot[index].GetComponent<SimpleMovement>().StartGame();
            endGame[index] = false;
        }

        public void AddDropObstacle(int indexPlayer, ValueCard ObstacleValue)
        {
            if (ObstacleValue == ValueCard.SpeedUp)
                ObstacleList[indexPlayer].Add(ObstacleValue);
            else
            {
             if (indexPlayer == 0)
                    ObstacleList[1].Add(ObstacleValue);
                else
                    ObstacleList[0].Add(ObstacleValue);
            }

        }

    public void DropObstacle(int indexPlayer, ValueCard ObstacleValue)
        {
            GameObject obstacle;
            switch (ObstacleValue)
            {
            case ValueCard.Boulders:
               
                obstacle= obManager.getDropObstacle(ObstacleValue).Spawn(new Vector3(Player[indexPlayer].transform.position.x, Player[indexPlayer].transform.position.y, Player[indexPlayer].transform.position.z + 15f));
                obstacle.GetComponent<SnowBall>().SetCamera(PlayerPivot[indexPlayer].transform.GetChild(1).transform);
                obstacle.GetComponent<SnowBall>().Send(-Vector3.forward);
                    break;
         
            case ValueCard.Freeze:
                StartCoroutine(Frost(indexPlayer));
                break;

            case ValueCard.SpeedUp:
                StartCoroutine(SpeedUp(indexPlayer,3.5f));
                break;

            case ValueCard.Pillars:
                obstacle = obManager.getDropObstacle(ObstacleValue).Spawn(new Vector3(Player[indexPlayer].transform.position.x, Player[indexPlayer].transform.position.y, Player[indexPlayer].transform.position.z + 15f));

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
                UIInGame.Instance<UIInGame>().LostLife(playerIndex, PlayerPivot[playerIndex].GetComponent<PlayerMovement>().GetLife());

                PlayerPivot[playerIndex].GetComponent<PlayerMovement>().LostLife();
                
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
                PlayerPivot[playerIndex].GetComponent<PlayerMovement>().LostLife();
                UIInGame.Instance<UIInGame>().LostLife(playerIndex, PlayerPivot[playerIndex].GetComponent<PlayerMovement>().GetLife());
                StartCoroutine(StartGame(playerIndex));
                break;
        }
        
        }

    public float getPlayerSpeed(int index)
    {
        return PlayerPivot[index].GetComponent<PlayerMovement>().speed;
    }

    public int getPlayerScore(int index)
    {
        return PlayerPivot[index].GetComponent<PlayerMovement>().getScore();
    }

}

