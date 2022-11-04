using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float HP;
    public float ActivationDistance;
    public float inactivationDistance;

    private NavMeshAgent agent;
    private Animator animator;
    private Collider2D collider;
    private SpriteRenderer renderer;
    private GameObject target;
    private Vector3 targetPosition;
    private Vector3 offset;
    private Vector3 initialPosition = new Vector3();
    private EnemyMovement enemyMovement;
    private EnemyAttack enemyAttack;
    private EnemyAudio enemyAudio;
    private bool dead;
    

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        offset = target.GetComponent<Collider2D>().offset;
        
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        renderer = GetComponent<SpriteRenderer>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAttack = GetComponent<EnemyAttack>();
        enemyAudio = GetComponent<EnemyAudio>();
        agent = GetComponent<NavMeshAgent>();

        initialPosition = transform.position;
    }

    void Update()
    {
        if(dead){
            StopMoving();
            return;
        }

        targetPosition = target.transform.position + offset;

        var targetDistance = GetDistanceFromTarget();
        if(targetDistance < ActivationDistance){            
            ChaseTarget();
            return;
        };

        if(targetDistance < inactivationDistance){
            BackToInit();
            return;
        };

        StopMoving();
    }

    private float GetDistanceFromTarget(){
        return  (transform.position - targetPosition).magnitude;
    }

    private void BackToInit(){
        enemyMovement.SetTargetPosition(initialPosition);
        enemyAudio.PlayWalkSound();
        enemyMovement.Move();
    }

    private void ChaseTarget(){
        enemyMovement.SetTargetPosition(targetPosition);
        
        if(!enemyAttack.CanAttack(targetPosition)){
            enemyAudio.PlayWalkSound();
            enemyMovement.Move();
            return;
        }

        var direction = enemyAttack.SetLinearShootDirection(targetPosition);
        enemyMovement.TurnToTarget(direction);
        enemyAttack.Attack();
        enemyAudio.PlayAttackSound();
    }

    private void StopMoving(){
        enemyMovement.Stop();
    }

    public void TakeDamage(float damage, Vector2 direction){
        HP -= damage;
        if(HP > 0){
            enemyMovement.AddImpulse(direction);
            enemyAudio.PlayHitSound();
            enemyAttack.ReloadAttack();
            return;
        }
        
        dead = true;
        enemyAudio.PlayDieSound();
        animator.SetBool("Dead", dead);
        StopMoving();

        enabled = false;
        renderer.sortingOrder = 0;
        collider.enabled = false;
    }
}
