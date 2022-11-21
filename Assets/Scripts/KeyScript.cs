using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public GameObject door;

    private ItemController itemController;
    private bool colliding;

    void Start()
    {
        itemController = GameObject.Find("ItemController").GetComponent<ItemController>();
    }
    
    void OnTriggerEnter2D(Collider2D collider) {
        if(colliding){
            return;
        }

        if(collider.tag.Equals("Player")){
            colliding = true;
            itemController.AddNewKey(gameObject);
            Destroy(gameObject);
        }

        colliding = false;
    }
}
