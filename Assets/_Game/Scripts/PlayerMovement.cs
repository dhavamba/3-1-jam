using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{

    
    public float speed = 4f;
    private float StartSpeed;
    public float JumpIntensity=300f;
    private float speedFactor = 0.05f;
    private Vector3 startPosition;
    Transform target;
    GameManager gm;

    // Use this for initialization
    void Start()
    {
        gm = GameManager.Instance<GameManager>();
        startPosition = transform.position;
        StartSpeed = speed;
        InvokeRepeating("IncreaseSpeed", 2.5f,2.5f);
    }

    public void StartGame()
    {
        transform.position=startPosition;
        speed = StartSpeed;
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        if (!gm.isEndGame())
        {
            
            if (Input.GetKeyDown("d"))
            {

                gm.ShiftleftLane();
               

            }
            else
            if (Input.GetKeyDown(KeyCode.Space))
            {

                gm.Jump(JumpIntensity);

            }
            else
            if (Input.GetKeyDown("a"))
            {

                gm.ShiftRightLane();
             

            }
            
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
           
        }
        
    }

    

    public void ResetTostart()
    {
       
        transform.position = startPosition;

    }
    public void IncreaseSpeed()
    {
        speed += speedFactor;
    }
    public void Slow(float slowFactor)
    {
        if(speed - slowFactor * speedFactor > StartSpeed/2)
            speed -= slowFactor * speedFactor;
    }

    


}
