using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeItem : MonoBehaviour
{
    public float amount;

    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private ItemControllerScript itemControllerScript;
    private bool colliding;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        itemControllerScript = GetComponent<ItemControllerScript>();
    }
    
    void OnTriggerEnter2D(Collider2D collider) {
        if(colliding){
            return;
        }

        if(collider.tag.Equals("Player")){
            colliding = true;
            GetItem(collider);
        }
    }

    private void GetItem(Collider2D collider){
        collider.gameObject.GetComponent<PlayerScript>().AddLife(amount);
        boxCollider.enabled = false;
        spriteRenderer.enabled = false;
        audioSource.Play();

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
