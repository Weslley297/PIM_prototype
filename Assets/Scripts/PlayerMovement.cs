using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator anim;

    private Vector2 movement = new Vector2();
    private readonly Vector3 LEFT = new Vector3(-1,1,1);
    private readonly Vector3 RIGHT = new Vector3(1,1,1);

    void Update()
    {
        movement = this.getLinearMovement();

        setStateParameters(movement);      

        transform.localScale = getLeftRight();
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    private Vector2 getLinearMovement(){
        Vector2 axle;
        axle.x = Input.GetAxisRaw("Horizontal");
        if(axle.x != 0){
            return new Vector2(axle.x, 0);
        }

        axle.y = Input.GetAxisRaw("Vertical");
        if(axle.y != 0){
           return new Vector2(0, axle.y);
        }

        return new Vector2(axle.x, axle.y);
    }

    private void setStateParameters(Vector2 direction){
        anim.SetFloat("Speed", direction.sqrMagnitude);  

        if(direction.x != 0 || direction.y != 0){
            anim.SetFloat("DirX", direction.x);
            anim.SetFloat("DirY", direction.y);  
        }
    }

    private Vector3 getLeftRight(){
        return movement.x == 0 ? transform.localScale
            : movement.x > 0 ? RIGHT : LEFT;
    }
    
}
