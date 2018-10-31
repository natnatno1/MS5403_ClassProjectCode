using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NC_Ship_Health : NC_FakePhysicsScripts {

    public GameObject GO_Explosion;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //When the ship collides with a rock, create an explosion at the ship's position, decrease the player's lives by 1, and then destroy itself
        if (collision.gameObject.tag == "Rock")
        {
            Instantiate(GO_Explosion, this.gameObject.transform.position, Quaternion.identity);
            GameObject.Find("GameManager").GetComponent<NC_Lives>().in_lives -= 1;
            Destroy(gameObject);
        }

    }
    
}
