using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAreaScript : MonoBehaviour
{
    public bool emitOnce;
    public string eventName = "";
    public string outEventName = "";
    private EventControllerScript eventController;
    private bool colliding;
    
    void Start()
    {
        eventController = GameObject.FindWithTag("GameController")
            .GetComponent<EventControllerScript>();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(colliding || string.IsNullOrEmpty(eventName)){
            return;
        }

        if(collider.tag.Equals("Player")){
            colliding = true;
            eventController.EventEmit(eventName);

            if(emitOnce){
                Destroy(gameObject);
            } 
        }
        
    }

    private void OnTriggerExit2D(Collider2D collider) {
        
        if(!colliding || string.IsNullOrEmpty(outEventName)){
            return;
        }

        if(collider.tag.Equals("Player")){
            colliding = false;
            eventController.EventEmit(outEventName);
            Destroy(gameObject);
        }
        
    }
}
