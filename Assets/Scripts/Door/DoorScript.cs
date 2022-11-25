using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private ItemControllerScript itemController;
    private Collider2D doorCollider;
    private AudioSource audioSource;
    private Animator animator;
    private float timer = 0;
    private bool locked = true;
    private bool open = false;

    void Start()
    {
        itemController = GameObject.FindWithTag("GameController")
            .GetComponent<ItemControllerScript>();
            
        animator = GetComponent<Animator>();
        doorCollider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();

        SetStateParameters();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.tag.Equals("Player")){
            OpenDoor();
        }
    }

    private void OpenDoor(){
        if(locked && itemController.UseTheKey(gameObject)){
            locked = false;
            SetStateParameters();
            audioSource.Play();
        }
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
