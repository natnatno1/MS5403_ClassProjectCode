﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NC_FakePhysicsScripts : MonoBehaviour {

    protected Rigidbody2D mRB2;       //We only want safe classes messing with this, as we control Physics

    protected SpriteRenderer mSR;     //May need this later


    [SerializeField]
    protected float Speed = 1.0f;

    [SerializeField]
    protected float MaxSpeed = 10.0f;

    [SerializeField]
    protected float RotationSpeed = 360.0f;

    protected Vector3 mVelocity = Vector3.zero;

    BoxCollider2D mTemp;

    protected Collider2D mC2D; // this works as all 2D colliders are based on this

    // Assign SpriteRenderer, Collider2D and Rigidbody2D, and set the Rigidbody2D to Kinematic
    protected virtual void Start()
    {

        mSR = gameObject.GetComponent<SpriteRenderer>(); //Grab SR assigned in IDE
        Debug.Assert(mSR != null, "Error:Missing SpriteRenderer");

        mC2D = gameObject.GetComponent<Collider2D>();
        Debug.Assert(mC2D != null, "Error:Missing Collider2D");
        mC2D.isTrigger = true;     //Set it to trigger in code

        mRB2 = gameObject.GetComponent<Rigidbody2D>();  //Add RidgidBody2D in Code
        mRB2.isKinematic = true;       //Don't use Physics as we'll do our own
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tNewPostion;        //Storage for new position
        DoMove();       //Call our child specifc move function
        if (DoWrap(out tNewPostion))
        {       //Check if we should wrap
            transform.position = tNewPostion;   //Yes we need to have new position
        }
    }

    //Clamp velocity to MaxSpeed
    protected Vector3 ClampedVelocity()
    {
        if (mVelocity.magnitude > MaxSpeed)
        {
            return mVelocity.normalized * MaxSpeed;
        }
        return mVelocity;
    }


    //DefaultMove applies the velocity scaled by Time.Deltatime
    protected virtual void DoMove()
    {
        transform.position += mVelocity * Time.deltaTime; //Work out new position
    }

    //Wraps the screen
    bool DoWrap(out Vector3 vNewPosition)
    {
        float tHeight = Camera.main.orthographicSize;  //Figure out what Camera can see
        float tWidth = Camera.main.aspect * tHeight;  //Use aspect ratio to work out Width
        bool tMoved = false;     //Default is no wrap
        vNewPosition = transform.position;
        if (vNewPosition.x > tWidth)
        {
            vNewPosition.x -= 2.0f * tWidth;     //If out of bounds reset position
            tMoved = true;      //We are wrapping
        }
        else if (vNewPosition.x < -tWidth)
        {
            vNewPosition.x += 2.0f * tWidth;
            tMoved = true;
        }
        if (vNewPosition.y > tHeight)
        {
            vNewPosition.y -= 2.0f * tHeight;
            tMoved = true;
        }
        else if (vNewPosition.y < -tHeight)
        {
            vNewPosition.y += 2.0f * tHeight;
            tMoved = true;
        }
        return tMoved;
    }

    //We are using triggers, so this gets called on overlap
    private void OnTriggerEnter2D(Collider2D vCollision)
    {
        NC_FakePhysicsScripts tOtherObject = vCollision.gameObject.GetComponent<NC_FakePhysicsScripts>();
        Debug.Assert(tOtherObject != null, "Other Object is not FakePhysics compatible");
        ObjectHit(tOtherObject);
    }


    //virtual functions can be overridded in derived classes
   protected virtual void ObjectHit(NC_FakePhysicsScripts vOtherObject)
   {
        Debug.LogFormat("{0} hit by {1}", name, vOtherObject.name);      //Just print it for now
   }
}

