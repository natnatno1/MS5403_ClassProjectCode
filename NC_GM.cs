using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NC_GM : MonoBehaviour {

    //Regions allow code to be folded

    #region GlobalVariables

    [SerializeField]
    private GameObject BulletPrefab;  //Link in IDE

    [SerializeField]
    private GameObject ShipPrefab;  //Link in IDE

    [SerializeField]
    private GameObject[] RockPrefabs;   //Link in IDE


    private int mScore;     //This Variable is private to GM
    public static int Score
    { //Make it availiable via a Getter
        get
        {
            return sGM.mScore;
        }
    }

    public Vector3 mSpawnPosition;
    public static Vector3 SpawnPosition
    {
        get
        {
            return sGM.mSpawnPosition;
        }
    }

    #endregion


    //Creating a singleton Design Pattern
    //A Singleton can be accesses anweher and its the same object for all
    //No just an instance (copy)
    //See http://csharpindepth.com/Articles/General/Singleton.aspx
    public static NC_GM sGM = null;     //The static reference to the Singleton, its null anyway, but good reminder

    //We want to be up and running right away, before other stuff starts
    void Awake()
    {
        if (sGM == null)
        {     //If we are doing this for the first time ONLY
            sGM = this;     //Now the static variable is reference to our instance
            DontDestroyOnLoad(sGM.gameObject);  //This means it survives a scene change
            InitialiseGame();
        }
        else if (sGM != this)
        {  //If we get called again and are not the same as before, kill duplicate
            Destroy(gameObject);
        }
    }
    
    #region
    //Create the player ship and the rocks when we start the game
    void InitialiseGame()
    {
        CreatePlayerShip();
        for (int tRockIndex = 0; tRockIndex < 5; tRockIndex++)
        {
            CreateRock(0);
        }
    }
    #endregion

    #region 
    //Create a ship
    public static void CreatePlayerShip()
    {
        Debug.Assert(sGM.ShipPrefab != null, "ShipPrefab prefab not linked in IDE");
        Instantiate(sGM.ShipPrefab, Vector3.zero, Quaternion.identity);
    }
    #endregion

    #region RockControl
    //Create different rocks for different events
    public static void CreateRock(int vIndex)
    {
       

        Debug.Assert(sGM.RockPrefabs != null, "RockPrefabs not linked in IDE");
        //Make sure the vIndex number is not a higher number that the highest vIndex number
        if (vIndex < sGM.RockPrefabs.Length)
        {
            //Create 1 large rock in a random position
            if (vIndex == 0)
            {
                Instantiate(sGM.RockPrefabs[vIndex], RandomScreenPositition, Quaternion.identity);
                Debug.LogFormat("Index {0}", vIndex);
            }

            //Create 2 medium rocks in the same position as the big rock was in when it was destroyed
            else if (vIndex == 1)
            {
                Instantiate(sGM.RockPrefabs[vIndex], SpawnPosition, Quaternion.identity);
                Instantiate(sGM.RockPrefabs[vIndex], SpawnPosition, Quaternion.identity);
                Debug.LogFormat("Index {0}", vIndex);
            }

            //Create 2 small rocks in the same position as the medium rock was in when it was destroyed
            else if (vIndex == 2)
            {
                Instantiate(sGM.RockPrefabs[vIndex], SpawnPosition, Quaternion.identity);
                Instantiate(sGM.RockPrefabs[vIndex], SpawnPosition, Quaternion.identity);
                Debug.LogFormat("Index {0}", vIndex);
            }
        }

        //If the vIndex number is higher than the highest vIndex number, send a message to the console
        else
        {
            Debug.LogFormat("RockPrefab Index {0} out of range", vIndex);
        }
    }
    
    #endregion

    //Create a bullet using the values defined in the "NC_Firing" script
    public static void CreateBullet(Vector3 vPosition, Vector3 vDirection)
    {
        Debug.Assert(sGM.BulletPrefab != null, "BulletPrefab not linked in IDE");
        NC_Bullet tBullet = Instantiate(sGM.BulletPrefab, vPosition, Quaternion.identity).GetComponent<NC_Bullet>();
        Debug.Assert(tBullet != null, "Bullet Script missing");
        tBullet.FireBullet(vPosition, vDirection);
    }


    #region Utilities
    //Set a random screen position for the initial rocks to spawn in
    public static Vector3 RandomScreenPositition
    {
        get
        {
            float tHeight = Camera.main.orthographicSize;  //Figure out what Camera can see
            float tWidth = Camera.main.aspect * tHeight;  //Use aspect ratio to work out Width
            return new Vector3(Random.Range(-tWidth, tWidth), Random.Range(-tHeight, tHeight), 0.0f);
        }
    }
    
    
    #endregion

    //Add the value defined in the "NC_Bullet2 script to the score
    public static void SetScore(int in_add_to_score)
    {
        sGM.mScore = (sGM.mScore += in_add_to_score);
        Debug.LogFormat("Score Increased");
    }


}
