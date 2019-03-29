﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    public Vector2 startForce;

    public GameObject nextBall;

    public GameObject[] balls;



    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(startForce, ForceMode2D.Impulse);
    }

    public void Split()
    {
        balls = GameObject.FindGameObjectsWithTag("Ball");
        //Debug.Log("Number of bubbles ---> " + balls.Length);

        if (nextBall != null)
        {
            GameObject ball1 = Instantiate(nextBall, rb.position + Vector2.right / 4f, Quaternion.identity);
            GameObject ball2 = Instantiate(nextBall, rb.position + Vector2.left / 4f, Quaternion.identity);

            ball1.GetComponent<BallMove>().startForce = new Vector2(2f, 5f);
            ball2.GetComponent<BallMove>().startForce = new Vector2(-2f, 5f);
        }
      

        Destroy(gameObject);
       // if (balls.Length == 1)
      //  {
           // Debug.Log("Game Over!");
     //   }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
