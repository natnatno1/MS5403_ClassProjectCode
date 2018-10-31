using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NC_Bullet : NC_FakePhysicsScripts {
    

    
    protected override void Start()
    {
        base.Start();   //Call FakePhysics Start()
        Destroy(gameObject, 2.0f);  //Destroy the bullet after 2 seconds
    }

    //Work out the initial velocity and position of the bullet
    public void FireBullet(Vector3 vPosition, Vector3 vDirection)
    {
        transform.position = vPosition;
        mVelocity = vDirection * Speed;
    }

    //Control the movement of the bullet
    protected override void DoMove()
    {
        transform.position += mVelocity * Time.deltaTime;      //Work out new position
    }

    //Control what happens when a bullet hits a rock
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        //When the bullet hits a big rock, save the position of the rock so we can spawn 2 medium ones, add 1 to the score, increase the vIndex number for the rock creation, and then destroy the bullet and the rock
        if (collision.gameObject.name == "Rock Big(Clone)")
        {
            GameObject.Find("GameManager").GetComponent<NC_GM>().mSpawnPosition = collision.gameObject.transform.position;
            NC_GM.SetScore(+1);
            NC_GM.CreateRock(+1);
            Destroy(collision.gameObject);
            Destroy(gameObject);
            
        }

        //When the bullet hits a medium rock, save the position of the rock so we can spawn 2 small ones, add 2 to the score, increase the vIndex number for the rock creation, and then destroy the bullet and the rock
        else if (collision.gameObject.name == "Rock Medium(Clone)")
        {
            GameObject.Find("GameManager").GetComponent<NC_GM>().mSpawnPosition = collision.gameObject.transform.position;
            NC_GM.SetScore(+2);
            NC_GM.CreateRock(+2);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        //When the bullet hits a small rock, add 3 to the score, destroy the bullet and the rock, and then send a message to the console saying the rock has been destroyed
        else if (collision.gameObject.name == "Rock Small(Clone)")
        {
            NC_GM.SetScore(+3);
            Destroy(collision.gameObject);
            Debug.LogFormat("Rock Destroyed");
            Destroy(gameObject);
        }
    }
}
