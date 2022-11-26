using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeItem : MonoBehaviour
{
    public float amount;

    private ItemControllerScript itemController;
    private bool colliding;

    private void Start() {
        itemController = GameObject.Find("GameController").GetComponent<ItemControllerScript>();
    }
    
    void OnTriggerEnter2D(Collider2D collider) {
        if(colliding){
            return;
        }

        if(collider.tag.Equals("Player")){
            colliding = true;
            itemController.CatchLifeItem();
            collider.gameObject.GetComponent<PlayerScript>().AddLife(amount);
            Destroy(gameObject);
        }
    }
}
