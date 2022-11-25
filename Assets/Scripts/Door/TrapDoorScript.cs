using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorScript : MonoBehaviour
{
    private Collider2D doorCollider;
    private AudioSource audioSource;
    private Animator animator;
    private float timer = 0;
    private bool locked = false;
    private bool open = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        doorCollider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();

        SetStateParameters();
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if(collider.tag.Equals("Player")){
            CloseDoor();
        }
    }

    private void CloseDoor(){
        if(open){
            open = false;
            doorCollider.isTrigger = false;
            
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

        locked = true;
        SetStateParameters();
        audioSource.Stop();
    }

    private void SetStateParameters(){
        animator.SetBool("Locked", locked);
        animator.SetBool("Open", open);
    }
}
