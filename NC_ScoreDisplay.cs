using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NC_ScoreDisplay : MonoBehaviour {

	//Display the score
	
	// Update is called once per frame
	void Update () {

        GetComponent<Text>().text = ("Score: " + NC_GM.Score);

	}
}
