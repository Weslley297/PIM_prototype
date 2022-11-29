
using System;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float HP;
    public GameObject dropItem;
    public float dropRate;

    private Animator enemyAnimator;
    private Collider2D enemyCollider;
    private SpriteRenderer enemyRenderer;
    private EnemyMovementScript enemyMovement;
    private EnemyAttackScript enemyAttack;
    private EnemyAudioScript enemyAudio;
    private EnemyAIScript enemyAI;
    private FadeOutEffectScript enemyFadeOutEffect;
    private float timer = 0;
    private bool dead;

    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();
        enemyRenderer = GetComponent<SpriteRenderer>();
        enemyMovement = GetComponent<EnemyMovementScript>();
        enemyAttack = GetComponent<EnemyAttackScript>();
        enemyAudio = GetComponent<EnemyAudioScript>();
        enemyAI = GetComponent<EnemyAIScript>();
        enemyFadeOutEffect = GetComponent<FadeOutEffectScript>();
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.rigidbody != null && collision.rigidbody.bodyType == RigidbodyType2D.Static){
            enemyMovement.WayBloqued();
        }

        if(collision.collider.tag == "Player"){
            enemyAudio.PlayAttackSound();
            
            var player = collision.gameObject.GetComponent<PlayerScript>();
            var direction = GetContactiDirection(collision);
            player.TakeDamage(10, -direction);
        }
    }

    private Vector2 GetContactiDirection(Collision2D collision){
        Vector2 contactPoint = collision.contacts[0].point;
        Vector2 colliderPosition = collision.collider.transform.position;

        var contactForce = (contactPoint - colliderPosition).normalized;
        var isHorizontal = Math.Abs(contactForce.x) > Math.Abs(contactForce.y);
        return isHorizontal 
            ? new Vector2(contactForce.x < 0 ? -1 : 1, 0)
            : new Vector2(0, contactForce.y < 0 ? -1 : 1);
    }

    void OnCollisionExit2D(Collision2D  collision)
    {
        if(collision.rigidbody != null && collision.rigidbody.bodyType == RigidbodyType2D.Static){
            enemyMovement.WayOpen();
        }
    }

    void Update()
    {
        if(timer < 0){
            timer += Time.deltaTime;
            return;
        }

        if(dead){
            DropTheItem();
            enemyFadeOutEffect.Activate();
            timer = -enemyFadeOutEffect.delay;
            return;
        }
    }

    private void DropTheItem(){
        var luck = UnityEngine.Random.value;
        Debug.Log(luck);
        if(luck <= dropRate){
            Debug.Log("Item");
            Instantiate(dropItem, transform.position, Quaternion.identity);
        }
    }
    
    public void TakeDamage(float damage, Vector2 direction)
    {
        if(timer < 0){
            return;
        }

        HP -= damage;
        if(HP <= 0){
            Die();
            return;
        }
        
        timer = -0.3f;
        enemyMovement.AddImpulse(direction, 1);
        enemyAudio.PlayHitSound();
        enemyAttack.ReloadAttack();
    }

    private void Die(){
        dead = true;
        timer = -5f;

        enemyRenderer.sortingOrder = 0;
        enemyCollider.enabled = false;
        enemyAnimator.SetBool("Dead", dead);

        enemyAudio.PlayDieSound();
        enemyMovement.Stop();
        enemyAI.Stop();
    }
}
