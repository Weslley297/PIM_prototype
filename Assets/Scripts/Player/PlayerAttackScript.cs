using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    public GameObject swordPrefab;

    private GameObject sword;
    private Animator animator;
    private float attackDelay = 0.6f;
    private float timer = 0;
    private bool attacking;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(!attacking){
            return;
        }

        if(timer < 0){
            timer += Time.deltaTime;
            return;
        }

        if(timer >= 0){
            attacking = false;
            SetAttackingParamater();
            Destroy(sword);
        }
    }

    public void SwordAttack(){
        if(swordPrefab == null && timer < 0){
            return;
        }

        attacking = true;
        CreateSword();
        SetAttackingParamater();
        timer = -attackDelay;
    }

    private void CreateSword(){
        sword = Instantiate(swordPrefab, transform.position, Quaternion.identity);
        sword.transform.parent = gameObject.transform;
       
        var swordScript = sword.GetComponent<SwordScript>();
        swordScript.SetSwordUser(gameObject);

        var direction =  GetPlayerDirection(); 
        swordScript.SetDirection(direction);
        swordScript.SetSwordPosition();
    }

    private Vector2 GetPlayerDirection(){
        return new Vector2(
            animator.GetFloat("DirX"), 
            animator.GetFloat("DirY")
        );
    }

    private void SetAttackingParamater(){
        animator.SetBool("Attacking", attacking);
    }

    public float GetAttackDelay(){
        return attackDelay;
    }

    public bool NotHaveASword(){
        return swordPrefab == null;
    }

    public void SetSword(GameObject swordObj){
        swordPrefab = swordObj;
    }
}
