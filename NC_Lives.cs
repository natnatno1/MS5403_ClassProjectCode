using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NC_Lives : MonoBehaviour {

    public int in_lives = 3;

	// When the game starts, display a ship image for every life the player has
	void Start () {

        GameObject.Find("Lives 1").GetComponent<Image>().enabled = true;
        GameObject.Find("Lives 2").GetComponent<Image>().enabled = true;
        GameObject.Find("Lives 3").GetComponent<Image>().enabled = true;
		
	}
	
	// Update is called once per frame
	void Update () {

        //When the player is down to 2 lives, make 1 ship invisible
        if (in_lives == 2)
        {
            GameObject.Find("Lives 3").GetComponent<Image>().enabled = false;
        }

        //When the player is down to 1 life, make 2 ships invisible
        if (in_lives == 1)
        {
            GameObject.Find("Lives 2").GetComponent<Image>().enabled = false;
        }

        //When the player has run out of lives, make all ships invisible
        if (in_lives == 0)
        {
            GameObject.Find("Lives 1").GetComponent<Image>().enabled = false;
        }
        
	}
}
