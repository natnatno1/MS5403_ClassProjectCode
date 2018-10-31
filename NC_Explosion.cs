using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NC_Explosion : MonoBehaviour {

	// When explostion commences, set the gameobject to be destroyed after 2 seconds (this leaves enough time for the coroutine to run), and initiate the coroutine
	void Start () {
        Destroy(gameObject, 2.0f);
        StartCoroutine(Respawn());
    }

    
    IEnumerator Respawn()
    {
        //If the player still has lives left, then create a new player ship after 1 second
        if (GameObject.Find("GameManager").GetComponent<NC_Lives>().in_lives > 0)
        {
            yield return new WaitForSeconds(1);
            NC_GM.CreatePlayerShip();
        }
    }
}
