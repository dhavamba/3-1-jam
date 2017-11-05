using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    
    public float speed = 4f;
    public float JumpIntensity=300f;
    private float speedFactor = 0.00125f;
    private Vector3 startPosition;
    Transform target;

    // Use this for initialization
    void Start()
    {
        target = GameManager.Instance<GameManager>().getCurrentLaneTarget();
        startPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown("d"))
        {

            GameManager.Instance<GameManager>().ShiftleftLane();
            target = GameManager.Instance<GameManager>().getCurrentLaneTarget();

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

            GameManager.Instance<GameManager>().Jump(JumpIntensity);

        }

        if (Input.GetKeyDown("a"))
        {

            GameManager.Instance<GameManager>().ShiftRightLane();
            target = GameManager.Instance<GameManager>().getCurrentLaneTarget();

        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        
    }


    public void UpdateSpeed()
    {
        speed += speedFactor;

    }

    public void ResetTostart()
    {
        transform.position = startPosition;

    }

    


}
