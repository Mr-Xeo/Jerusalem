using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrollingManager : MonoBehaviour
{
    private float picLength;
    private float picSpeed;
    public float parallaxSpeed;

    private float walkingTime;
    private float stopTime;

    void Start()
    {
        picLength = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        if (Player.isWalking)
        {
            walkingTime = Time.fixedTime - stopTime;


            picSpeed = -parallaxSpeed * walkingTime;
            transform.position = new Vector2(Mathf.Repeat(picSpeed, picLength), transform.position.y);
        }

        else
        {
            stopTime = Time.fixedTime - walkingTime;
        }
    }
}
