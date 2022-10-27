using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float ActivationDistance;
    public float inactivationDistance;

    private NavMeshAgent agent;
    private GameObject target;
    private Vector3 targetPosition;
    private Vector3 targetOffset;
    private Vector3 initialPosition = new Vector3();
    private EnemyMovement enemyMovement;
    private EnemyAttack enemyAttack;
    

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        targetOffset = target.GetComponent<Collider2D>().offset;
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAttack = GetComponent<EnemyAttack>();
        agent = GetComponent<NavMeshAgent>();

        initialPosition = transform.position;
    }

    void Update()
    {
        targetPosition = target.transform.position + targetOffset;

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
        enemyMovement.Move();
    }

    private void ChaseTarget(){
        enemyMovement.SetTargetPosition(targetPosition);
        
        if(!enemyAttack.CanAttack(targetPosition)){
            enemyMovement.Move();
            return;
        }

        var direction = enemyAttack.SetLinearShootDirection(targetPosition);
        enemyMovement.TurnToTarget(direction);
        enemyAttack.Attack();
    }

    private void StopMoving(){
        enemyMovement.Stop();
    }
}
