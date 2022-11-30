using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool isSolar;
    private ItemControllerScript itemController;
    private EventControllerScript eventController;
    private Collider2D doorCollider;
    private AudioSource audioSource;
    private Animator animator;
    private bool firstTime = true;
    private float timer = 0;
    private bool locked = true;
    private bool open = false;

    void Start()
    {
        var gameController = GameObject.FindWithTag("GameController");
        itemController = gameController.GetComponent<ItemControllerScript>();
        eventController = gameController.GetComponent<EventControllerScript>();
            
        animator = GetComponent<Animator>();
        doorCollider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();

        SetStateParameters();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.tag.Equals("Player")){
            TryOpenDoor();
        }
    }

    private void TryOpenDoor(){
        if(locked && itemController.UseTheKey(gameObject)){
            OpenDoor();
            return;
        }

        if(!firstTime){
            return;
        }

        var eventName = isSolar ? "SolarDoorDialogEvent": "LockedDoorDialogEvent";
        eventController.EventEmit(eventName);
        firstTime = false;
    }

    public void OpenDoor(){
        locked = false;
        SetStateParameters();
        audioSource.Play();
    }

    void Update()
    {
        if(locked || open){
            return;
        }

        timer += Time.deltaTime;
        if(timer < 0.6){
            return;
        }

        open = true;
        SetStateParameters();
        Destroy(doorCollider);
        audioSource.Stop();
    }

    private void SetStateParameters(){
        animator.SetBool("Locked", locked);
        animator.SetBool("Open", open);
    }
}
