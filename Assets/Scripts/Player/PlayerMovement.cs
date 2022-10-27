using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float damageImpulse;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement = new Vector2();
    
    private readonly Vector3 LEFT = new Vector3(-1,1,1);
    private readonly Vector3 RIGHT = new Vector3(1,1,1);
    private float timer;

    private void Start() 
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.collider.tag == "Enemy" || collision.collider.tag == "Bullet"){
            ContactImpulse(collision.collider.transform.position, collision.contacts[0].point);
            timer = 0;
        }
    }

    private void ContactImpulse(Vector2 colliderPosition, Vector2 contactPoint){
        var contactForce = (contactPoint - colliderPosition).normalized;
        var contactDirection = GetContactiDirection(contactForce);
    
        rb.AddForce(contactDirection * damageImpulse, ForceMode2D.Impulse);
    }

    private Vector2 GetContactiDirection(Vector2 contact){
        var isHorizontal = Math.Abs(contact.x) > Math.Abs(contact.y);
        if(isHorizontal)
        {
            return new Vector2(contact.x < 0 ? -1 : 1, 0);
        }

        return new Vector2(0, contact.y < 0 ? -1 : 1);
    }


    void Update() {
        movement = this.getLinearMovement();

        setStateParameters(movement);      

        transform.localScale = getTranslationLeftOrRight();
    }

    void FixedUpdate() {
        if(timer < 0.3){
            timer += Time.deltaTime;
            return;
        }

        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    private Vector2 getLinearMovement(){
        var inputHorizontal = Input.GetAxisRaw("Horizontal");
        if(inputHorizontal != 0){
            return new Vector2(inputHorizontal, 0);
        }

        var inputVertical = Input.GetAxisRaw("Vertical");
        if(inputVertical != 0){
           return new Vector2(0, inputVertical);
        }

        return new Vector2(0, 0);
    }

    private void setStateParameters(Vector2 direction){
        var speed = direction.sqrMagnitude;
        animator.SetFloat("Speed", direction.sqrMagnitude);  

        if(speed > 0){
            animator.SetFloat("DirX", direction.x);
            animator.SetFloat("DirY", direction.y);  
        }
    }

    private Vector3 getTranslationLeftOrRight(){
        return movement.x == 0 ? transform.localScale
            : movement.x > 0 ? RIGHT : LEFT;
    }
    
}
