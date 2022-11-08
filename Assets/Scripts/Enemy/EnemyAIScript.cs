using UnityEngine;
using UnityEngine.AI;

public class EnemyAIScript : MonoBehaviour
{
    public float ActivationDistance;
    public float inactivationDistance;

    private GameObject target;
    private Vector3 targetPosition;
    private Vector3 offset;
    private Vector3 initialPosition = new Vector3();
    private EnemyMovementScript enemyMovement;
    private EnemyAttackScript enemyAttack;
    private EnemyAudioScript enemyAudio;
    private bool stoppped;
    

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        offset = target.GetComponent<Collider2D>().offset;
        
        enemyMovement = GetComponent<EnemyMovementScript>();
        enemyAttack = GetComponent<EnemyAttackScript>();
        enemyAudio = GetComponent<EnemyAudioScript>();

        initialPosition = transform.position;
    }

    void Update()
    {
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

        enemyMovement.Stop();
    }

    private float GetDistanceFromTarget(){
        return  (transform.position - targetPosition).magnitude;
    }

    private void ChaseTarget(){
        enemyMovement.SetTargetPosition(targetPosition);
        
        if(!enemyAttack.CanAttack(targetPosition)){
            enemyAudio.PlayWalkSound();
            enemyMovement.Move();
            return;
        }

        enemyAttack.SetAttackDirection(targetPosition);
        enemyMovement.TurnToDirection(enemyAttack.getAttackDirection());
        enemyMovement.StopByTime(0.5f);
        enemyAudio.PlayAttackSound();
        enemyAttack.Attack();
    }

    private void BackToInit(){
        enemyMovement.SetTargetPosition(initialPosition);
        enemyAudio.PlayWalkSound();
        enemyMovement.Move();
    }

    public void Stop(){
        enabled = false;
    }
}
