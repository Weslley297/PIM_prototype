using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject swordPrefab;

    private GameObject sword;
    private PlayerMovement movement;
    private Animator animator;
    private float timer;
    private float attackDelay = 0.3f;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(timer < 0){
            timer += Time.deltaTime;
            return;
        }

        if(sword != null){
            SetAttacking(false);
            Destroy(sword);
        }

        if (Input.GetButtonDown("Attack")){
            SwordAttack();
            return;
        }

    }

    private void SwordAttack(){
        if(swordPrefab == null){
            return;
        }

        CreateSword();
        SetAttacking(true);
        
        movement.StopMoviment(attackDelay);
        timer = -attackDelay;
    }

    private void CreateSword(){
        sword = Instantiate(swordPrefab, transform.position, Quaternion.identity);
        sword.transform.parent = gameObject.transform;
       
        var swordScript = sword.GetComponent<SwordScript>();
         Vector2 direction =  new Vector2(
            animator.GetFloat("DirX"), 
            animator.GetFloat("DirY")
        );
         
        swordScript.SetDirection(direction);
        swordScript.SetSwordUser(gameObject);
    }

    private void SetAttacking(bool attacking){
        animator.SetBool("Attacking", attacking);
    }
}
