using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    public GameObject swordPrefab;

    private GameObject sword;
    private Animator animator;
    private float timer;
    private float attackDelay = 0.3f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(timer < 0){
            timer += Time.deltaTime;
            return;
        }

        if(sword != null){
            SetAttackingParamater(false);
            Destroy(sword);
        }
    }

    public void SwordAttack(){
        if(swordPrefab == null){
            return;
        }

        CreateSword();
        SetAttackingParamater(true);
        timer = -attackDelay;
    }

    private void CreateSword(){
        sword = Instantiate(swordPrefab, transform.position, Quaternion.identity);
        sword.transform.parent = gameObject.transform;
       
        var swordScript = sword.GetComponent<SwordScript>();
        swordScript.SetSwordUser(gameObject);

        var direction =  GetPlayerDirection(); 
        swordScript.SetDirection(direction);
    }

    private Vector2 GetPlayerDirection(){
        return new Vector2(
            animator.GetFloat("DirX"), 
            animator.GetFloat("DirY")
        );
    }

    private void SetAttackingParamater(bool attacking){
        animator.SetBool("Attacking", attacking);
    }

    public float GetAttackDelay(){
        return attackDelay;
    }
}
