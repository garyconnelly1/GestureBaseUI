﻿using System.Collections;
using System.Collections.Generic;
using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using Assets.Scripts;

using UnityEngine;
using UnityEngine.SceneManagement;

public class BubbleMove : MonoBehaviour
{
    private LevelsManager manager;
    private GameObject myo = null;
    public float playerSpeed = 7f;

    public Rigidbody2D rb;

    private float movement = 0f;


    GameObject[] bubbles;

    void Start()
    {
        manager = new LevelsManager();
        myo = GameObject.FindWithTag("myo");

    }


    // Update is called once per frame
    void Update()
    {
       // Player_Score.isOver = false;
        movement = Input.GetAxisRaw("Horizontal") * playerSpeed;
       
    }

    void FixedUpdate()
    {
       // Move();
        MyoMove();
    }

    void Move()
    {
        rb.MovePosition(rb.position + new Vector2(movement * Time.fixedDeltaTime, 0f));
    }

    void MyoMove()
    {
        // Access the ThalmicMyo component attached to the Myo game object.
        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();

        if (thalmicMyo.pose == Pose.WaveIn)
        {
            movement = -1.0f * playerSpeed;
        }
        else if(thalmicMyo.pose == Pose.Fist)
        {
            Chain.IsFired = true;
        }
        else if (thalmicMyo.pose == Pose.WaveOut)
        {
            movement = 1.0f * playerSpeed;
        }
        else if (thalmicMyo.pose == Pose.Rest)
        {

            movement = 0.0f;
        }
        else if (thalmicMyo.pose == Pose.FingersSpread)
        {
            Chain.IsFired = true;
        }
        rb.MovePosition(rb.position + new Vector2(movement * Time.fixedDeltaTime, 0f));
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Ball")
        {
            Debug.Log("GAME OVER!");
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            manager.ChangeLevel(Levels.Levels_Menu); // So any time the player dies, they get redirected back to the levels menu.
        }
    }

    // Extend the unlock if ThalmcHub's locking policy is standard, and notifies the given myo that a user action was
    // recognized.
    void ExtendUnlockAndNotifyUserAction(ThalmicMyo myo)
    {
        ThalmicHub hub = ThalmicHub.instance;

        if (hub.lockingPolicy == LockingPolicy.Standard)
        {
            myo.Unlock(UnlockType.Timed);
        }

        myo.NotifyUserAction();
    }
}
