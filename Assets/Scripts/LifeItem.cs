using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeItem : MonoBehaviour
{
    public float amount;
    private bool colliding;
    
    void OnTriggerEnter2D(Collider2D collider) {
        if(colliding){
            return;
        }

        if(collider.tag.Equals("Player")){
            colliding = true;
            collider.gameObject.GetComponent<PlayerScript>().AddLife(amount);
            Destroy(gameObject);
        }

        colliding = false;
    }
}
