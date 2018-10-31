﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NC_RockBehaviour : NC_FakePhysicsScripts {



  private float mRotation = 0.0f; //Rotation Speed


    // Use this for initialization
    protected override void Start()
    {
        base.Start(); //Call FakePhysics Start()

        //Now we can do our own Rock specific init
        mVelocity = new Vector3(Random.Range(-Speed, Speed), Random.Range(Speed, Speed), 0);
        mRotation = Random.Range(-RotationSpeed, RotationSpeed);

    }

    protected override void DoMove()
    {
        transform.Rotate(0, 0, mRotation * Time.deltaTime);    //Rotate Rock
        base.DoMove(); //Call Base class which will apply Velocity
    }
    





}




