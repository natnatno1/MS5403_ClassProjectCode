using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Displays a "Game Over" when the player runs out of lives
public class NC_GameOver : MonoBehaviour {

	// On start, make the text invisible
	void Start () {
        GetComponent<Text>().enabled = false;
    }
	
	// Once the player's life count is 0, make the text visible
	void Update () {
		if (GameObject.Find("GameManager").GetComponent<NC_Lives>().in_lives == 0)
        {
            GetComponent<Text>().enabled = true;
        }
	}
}
