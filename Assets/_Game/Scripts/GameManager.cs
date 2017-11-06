using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    // Use this for initialization
    public Transform[] lanes;
    private  Transform currentLane;
    private int indexLane;
    public GameObject playerPivot;
    public GameObject player;
    ObstaclesManager obManager;

    public Transform Target;
	void Start ()
    {
        obManager = GetComponent<ObstaclesManager>();

    }
    private void Awake()
    {
        indexLane = 1;
        currentLane = lanes[indexLane];
    }

    public void ShiftRightLane()
    {
        if (indexLane > 0 && player.GetComponent<PlayerCollider>().isGround())
        {
            indexLane--;
            currentLane = lanes[indexLane];
            player.transform.position=new Vector3(currentLane.position.x, player.transform.position.y, player.transform.position.z);
            player.transform.Rotate(0, 0, 0);
        }
    }

    public void ShiftleftLane()
    {
        if (indexLane < lanes.Length- 1  && player.GetComponent<PlayerCollider>().isGround())
        {
            indexLane++;
            currentLane = lanes[indexLane];
            player.transform.position = new Vector3(currentLane.position.x, player.transform.position.y, player.transform.position.z);
            player.transform.Rotate(0, 0, 0);
        }
    }

    public void Jump(float intensity)
    {
        if (player.GetComponent<PlayerCollider>().isGround())
        {
            player.GetComponent<Rigidbody>().AddForce(Vector3.up * intensity);
            player.transform.Rotate(0, 0, 0);
        }
    }

    public Transform getCurrentLaneTarget()
    {
        return lanes[indexLane].GetChild(0).transform;
    }
    // Update is called once per frame
    void Update () {

        if (Vector3.Distance(playerPivot.transform.position, Target.position) < 10f)
        {
            Debug.Log("Reset");
            obManager.ResetObstaclesArea();
            ResetPlayerPos();
            obManager.SpawObject();
        }
    }

    void ResetPlayerPos()
    {
        playerPivot.GetComponent<PlayerMovement>().ResetTostart();
        
    }
}
