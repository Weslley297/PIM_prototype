using System;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public float moveSpeed;
    public float damageImpulse;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement = new Vector2(0, 1);
    
    private readonly Vector3 LEFT = new Vector3(-1,1,1);
    private readonly Vector3 RIGHT = new Vector3(1,1,1);
    private float timer;

    private void Start() 
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void AddImpulse(Vector2 direction, float time){
        rb.AddForce(direction * damageImpulse, ForceMode2D.Impulse);
        SetMovement(-direction);
        animator.SetBool("Damaged", true); 
        timer = -time;
    }

    private void FixedUpdate() {
        setStateParameters(movement);      

        if(timer < 0){
            timer += Time.deltaTime;
            return;
        }

        animator.SetBool("Damaged", false);  
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }


    private void setStateParameters(Vector2 direction){
        var speed = direction.sqrMagnitude;
        animator.SetFloat("Speed", direction.sqrMagnitude);  

        if(speed > 0){
            animator.SetFloat("DirX", direction.x);
            animator.SetFloat("DirY", direction.y);  
        }  
    }

    public void StopByTime(float time){
        movement = new Vector2(0, 0);
        timer = -time;
    }

    public void SetMovement(Vector2 mov){
        movement = mov;
    }

    public void SetMoveSpeed(float speed){
        moveSpeed = speed;
    }
}
