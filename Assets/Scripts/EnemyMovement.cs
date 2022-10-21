using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed;
    public float chaseDistance;
    public float movimentDelay;
    public float stopDelay;

    private Animator anim;
    private NavMeshAgent agent;
    private GameObject target;
    private Vector3 movement = new Vector3();
    private Vector3 initalPosition = new Vector3();
    private float delay = 0;
    private bool isBloqued = false;   
    private bool movingToX;
    private bool chasing;

    private void Start() 
    {
        initalPosition = transform.position;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.updatePosition = false;

        target = GameObject.Find("Player");
    }

    void OnCollisionEnter2D(Collision2D  collision) 
    {
        isBloqued = true;
    }

    void OnCollisionExit2D(Collision2D  collision)
    {
        isBloqued = false;
        delay = 0;
    }

    void FixedUpdate()
    {
        delay += Time.deltaTime;
        if(delay < stopDelay){
            return;
        }

        agent.SetDestination(target.transform.position);

        var distanceFromTarget = (transform.position - target.transform.position).magnitude;
        chasing = distanceFromTarget < chaseDistance;

        if(!chasing){
            agent.isStopped = true;
            return;
        }

        agent.isStopped = false;
        var nextPosition = transform.position + movement;
        transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime);  
        if(delay < movimentDelay){
            return;
        }
        
        var distance = agent.nextPosition - transform.position;
        movement = getLinearMovement(distance);

        this.setStateParameters();
        delay = 0;
    }

    private Vector3 getLinearMovement(Vector3 distance){
        float movementDistance = 0;
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;

        var moveToX = Math.Abs(distance.x) > Math.Abs(distance.y);

        movingToX = moveToX;
        if(movingToX == !isBloqued)
        {
            movementDistance = distance.x < 0 ? -moveSpeed : moveSpeed;
            return new Vector3(movementDistance, 0, 0);
        }

        movementDistance = distance.y < 0 ? -moveSpeed : moveSpeed;
        return new Vector3(0, movementDistance, 0);
    }

    private void setStateParameters(){
        anim.SetFloat("DirX", movement.x);
        anim.SetFloat("DirY", movement.y);
    }
}