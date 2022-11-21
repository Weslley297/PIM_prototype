
using System;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float HP;
    public float maxHP;

    private Animator playerAnimator;
    private Collider2D playerCollider;
    private SpriteRenderer playerRenderer;
    private PlayerInputScript playerInput;
    private PlayerMovementScript playerMovement;
    private PlayerAttackScript playerAttack;
    private PlayerAudioScript playerAudio;
    private float timer = 0;
    private bool invulnerable;
    private bool dead;
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<Collider2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInputScript>();
        playerMovement = GetComponent<PlayerMovementScript>();
        playerAttack = GetComponent<PlayerAttackScript>();
        playerAudio = GetComponent<PlayerAudioScript>();
    }

    void Update()
    {
        if(timer < 0){
            timer += Time.deltaTime;
        }

        if(timer < 0 && invulnerable){
            ToogleSpriteVisible();
            return;
        }

        if(invulnerable) {
            MakeVulnerable();
        }      
    }

    private void ToogleSpriteVisible(){
        playerRenderer.enabled = !playerRenderer.enabled;
    }

    private void MakeVulnerable(){
        invulnerable = false;
        playerRenderer.enabled = true;
    }

    public void TakeDamage(float damage, Vector2 direction)
    {
        if(invulnerable){
            return;
        }

        HP -= damage;
        if(HP <= 0){
            Die();
            return;
        }
        
        timer = -1f;
        invulnerable = true;
        var delay = 0.3f;
        playerInput.InputDisableByTime(delay);
        playerMovement.AddImpulse(direction, delay);
        playerAudio.PlayHitSound();
    }

    private void Die(){
        dead = true;
        timer = -5f;

        playerRenderer.sortingOrder = 0;
        playerCollider.enabled = false;
        // playerAnimator.SetBool("Dead", dead);

        playerMovement.SetMoveSpeed(0);
        playerAudio.PlayDieSound();
        playerInput.InputDisable();
    }

    public void AddLife(float value){
        HP += value;
        
        if(HP > maxHP){
            HP = maxHP;
        }
    }
}
