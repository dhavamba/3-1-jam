using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public float speed = 4f;
    private float StartSpeed;
    public float JumpIntensity = 300f;
    private float speedFactor = 0.05f;
    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
        StartSpeed = speed;
        InvokeRepeating("IncreaseSpeed", 2.5f, 2.5f);
    }

    public void StartGame()
    {
        transform.position = startPosition;
        speed = StartSpeed;
    }

    protected virtual void Update()
    {
        Translate();
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
        if (speed - slowFactor * speedFactor > StartSpeed / 2)
            speed -= slowFactor * speedFactor;
    }

    protected void Translate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
