using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private ItemController itemController;
    private Collider2D doorCollider;
    public AudioSource audioSource;
    private Animator animator;
    private float timer = 0;
    private bool locked = true;
    private bool open = false;

    void Start()
    {
        itemController = GameObject.Find("ItemController")
            .GetComponent<ItemController>();
            
        animator = GetComponent<Animator>();
        doorCollider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
         if(collision.collider.tag.Equals("Player")){
            OpenDoor();
        }
    }

    private void OpenDoor(){
        if(locked && itemController.UseTheKey(gameObject)){
            locked = false;
            animator.SetBool("Locked", false);
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
        animator.SetBool("Open", true);
        Destroy(doorCollider);
        audioSource.Stop();
    }
}
