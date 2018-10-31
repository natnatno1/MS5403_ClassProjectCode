using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This Script also contains the code for the movement of the player ship
public class NC_Firing : NC_FakePhysicsScripts
{
    [SerializeField]
    private Transform GunPosition;


    //The ship just moves by simply adding the velocity & rotation
    protected override void DoMove()
    {
        float tThrust = Input.GetAxis("Vertical") * Speed;       //Get Thrust
        float tRotate = Input.GetAxis("Horizontal") * RotationSpeed; //Get Rotation
        transform.Rotate(0, 0, tRotate * Time.deltaTime);    //Rotate ship
        mVelocity += Quaternion.Euler(0, 0, transform.rotation.z) * transform.up * tThrust * Speed; //Non mass velocity
        mVelocity = ClampedVelocity();
        base.DoMove();  //Call Base class which will apply Velocity
        DoFiring();
    }

    //Creates the bullet if/when the spacebar is down, using the shown values
    void DoFiring()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NC_GM.CreateBullet(GunPosition.position, transform.up * Mathf.Max(4.0f, mVelocity.magnitude));
        }
    }
}
