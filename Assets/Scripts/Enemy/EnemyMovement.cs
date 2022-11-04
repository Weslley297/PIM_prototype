using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed;
    public float movimentDelay;
    public float stopDelay;
    public float damageImpulse;
    public bool linearMoviment;

    private Rigidbody2D rb;
    private Animator animator;
    private NavMeshAgent agent;
    private Vector3 targetPosition;
    private Vector2 movement = new Vector2();
    private float timer = 0;
    private bool notBloqued;

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.updatePosition = !linearMoviment;
    }

    void OnCollisionEnter2D(Collision2D  collision) 
    {
        if(collision.rigidbody.bodyType == RigidbodyType2D.Static){
            notBloqued = false;
        }
    }

    public void AddImpulse(Vector2 direction){
        rb.AddForce(direction * damageImpulse, ForceMode2D.Impulse);
        timer = -1;
    }

    private Vector2 GetContactiDirection(Vector2 contact){
        var isHorizontal = Math.Abs(contact.x) > Math.Abs(contact.y);
        if(isHorizontal)
        {
            return new Vector2(contact.x < 0 ? -1 : 1, 0);
        }

        return new Vector2(0, contact.y < 0 ? -1 : 1);
    }

    void OnCollisionExit2D(Collision2D  collision)
    {
        if(collision.rigidbody.bodyType == RigidbodyType2D.Static){
            notBloqued = true;
        }
    }

    void FixedUpdate() {
        if(agent.isStopped){
            return;
        }

        agent.SetDestination(targetPosition);
        
        if(linearMoviment){
            DoLinearMoviment();
        }
        
        this.SetStateParameters();
    }

    private void SetStateParameters(){
        animator.SetFloat("DirX", movement.x);
        animator.SetFloat("DirY", movement.y);
    }

    private void DoLinearMoviment(){
        if(timer < stopDelay){
            timer += Time.deltaTime;
            return;
        }

        rb.MovePosition(rb.position + movement * Time.deltaTime);
        if(timer < movimentDelay){
            timer += Time.deltaTime;
            return;
        }
        
        var distanceBetween = agent.nextPosition - transform.position;
        movement = GetLinearMovement(distanceBetween);
        timer = 0;
    }

    private Vector2 GetLinearMovement(Vector3 distanceBetween){
        float movementDistance = 0;
        var isHorizontalMovement = Math.Abs(distanceBetween.x) > Math.Abs(distanceBetween.y);

        if(isHorizontalMovement != notBloqued)
        {
            movementDistance = distanceBetween.x < 0 ? -moveSpeed : moveSpeed;
            return new Vector2(movementDistance, 0);
        }

        movementDistance = distanceBetween.y < 0 ? -moveSpeed : moveSpeed;
        return new Vector2(0, movementDistance);
    }

    public void SetTargetPosition(Vector3 position){
        targetPosition = position;
    }

    public void TurnToTarget(Vector2 direction){
        movement = direction;
        SetStateParameters();
        timer = -0.5f;
    }

    public void Stop(){
        if(agent.isStopped){
            return;
        }

        agent.isStopped = true;
        timer = -0;
    }

    public void Move(){
        if(!agent.isStopped){
            return;
        }

        agent.isStopped = false;
    }
}