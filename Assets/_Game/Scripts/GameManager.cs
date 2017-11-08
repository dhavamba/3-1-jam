using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public Transform[] lanes;
    private  Transform currentLane;
    private int indexLane;
    public GameObject playerPivot;
    public GameObject player;
    ObstaclesManager obManager;
    private bool endGame = false;


    public Transform Target;
	void Start ()
    {
        obManager = GetComponent<ObstaclesManager>();

    }

    public bool isEndGame()
    {
        return endGame;
    }
    private void Awake()
    {
        indexLane = 1;
        currentLane = lanes[indexLane];
    }

    public void ShiftRightLane()
    {
        if (indexLane > 0)
        {
            indexLane--;
            currentLane = lanes[indexLane];
            player.transform.Rotate(0, 0, 0);
        }
    }

   

    public void ShiftleftLane()
    {
        if (indexLane < lanes.Length- 1  )
        {
            indexLane++;
            currentLane = lanes[indexLane];
            player.transform.Rotate(0, 0, 0);
        }
    }

    public void Jump(float intensity)
    {
        if (player.GetComponent<PlayerCollider>().isGround())
        {
            player.GetComponent<Rigidbody>().AddForce(Vector3.up * intensity, ForceMode.Impulse);
            player.transform.Rotate(0, 0, 0);
        }
        
    }

    public Transform getCurrentLaneTarget()
    {
        return lanes[indexLane].GetChild(0).transform;
    }
    // Update is called once per frame
    void Update ()
    {

        if (Vector3.Distance(playerPivot.transform.position, Target.position) < 2f)
        {
            Debug.Log("Reset");
            obManager.ResetObstaclesArea();
            ResetPlayerPos();
            obManager.SpawObject();
        }


    }

    void ResetPlayerPos()
    {
        playerPivot.GetComponent<SimpleMovement>().ResetTostart();
        
    }

    void StartGame()
    {
        playerPivot.GetComponent<SimpleMovement>().StartGame();
        endGame = false;
    }

    public void ObstacleTriggered(GameObject collider)
    {
        switch(collider.ReturnTags()[0])
        {
            case "Truck":
                endGame = true;
                Invoke("StartGame", 1.5f);
                break;

            case "SnowMan":
                collider.GetComponent<SnowMan>().SnowImpact();
                playerPivot.GetComponent<PlayerMovement>().Slow(collider.GetComponent<SnowMan>().getSlowFactor());
                break;
        }
    }
}
