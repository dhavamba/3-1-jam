using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // Use this for initialization
    public static GameManager instance;
    public Transform[] lanes;
    private  Transform currentLane;
    private int indexLane;
    public GameObject playerPivot;
    public GameObject player;

    public Transform Target;
	void Start ()
    {
         
	}
    private void Awake()
    {
        instance = this;
        indexLane = 1;
        currentLane = lanes[indexLane];

    }

    public void ShiftRightLane()
    {
        if (indexLane > 0)
        {
            indexLane--;
            currentLane = lanes[indexLane];
            player.transform.position=new Vector3(currentLane.position.x, player.transform.position.y, player.transform.position.z);
        }
    }

    public void ShiftleftLane()
    {
        if (indexLane < lanes.Length-1)
        {
            indexLane++;
            currentLane = lanes[indexLane];
            player.transform.position = new Vector3(currentLane.position.x, player.transform.position.y, player.transform.position.z);
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
            ResetPlayerPos();
        }
    }

    void ResetPlayerPos()
    {
        //player.transform.localPosition = Vector3.zero;
        playerPivot.transform.position = Vector3.zero;
        
    }
}
