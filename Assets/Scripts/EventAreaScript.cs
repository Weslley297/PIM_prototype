using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAreaScript : MonoBehaviour
{
    public bool emitOnce;
    public string eventName = "";
    private EventControllerScript eventController;
    private bool colliding;
    
    void Start()
    {
        eventController = GameObject.FindWithTag("GameController")
            .GetComponent<EventControllerScript>();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(colliding){
            return;
        }

        if(collider.tag.Equals("Player")){
            colliding = true;
            eventController.EventEmit(eventName);
            Destroy(gameObject);
        }
        
    }
}
