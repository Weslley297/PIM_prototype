using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public float damage;

    private GameObject swordUser;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Vector3 position;
    private BoxCollider2D swordcollider;
    private Vector2 direction;
    private float timer;
    private bool colliding;

    private void Start() {
        sprite = GetComponent<SpriteRenderer>();
        swordcollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        swordcollider.enabled = false;
        timer = -0.6f;
    }

    void OnTriggerEnter2D(Collider2D otherCollider) 
    {
        if(colliding){
            return;
        }

        if(otherCollider.tag == "Enemy"){
            colliding = true;
            otherCollider.gameObject
                .GetComponent<EnemyScript>()
                .TakeDamage(damage, direction);
        }

        if(otherCollider.tag == "Destructible"){
            colliding = true;
            otherCollider.gameObject
                .GetComponent<DestructibleScript>()
                .Destruct();
        }

        colliding = false;
    }

    void Update()
    {
        transform.position = swordUser.transform.position + position;
        if(timer < -0.4){
            timer += Time.deltaTime;
            return;
        }

        if(timer <= 0){
            swordcollider.enabled = true;
            timer += Time.deltaTime;
            return;
        }

        if(timer > 0){
            Destroy(gameObject);
        }
    }

    public void SetSwordPosition(){
        if(direction.y < 0){
            position = new Vector3(0, -1.0f, 0);
            transform.Rotate(new Vector3(0, 0, 180f));
        }

        if(direction.y > 0){
            position = new Vector3(0, 1f, 0);
            transform.Rotate(new Vector3(0, 0, 0));
        }

        if(direction.x < 0){
            position = new Vector3(-1, 0, 0);
            transform.Rotate(new Vector3(0, 0, 270));
        }

        if(direction.x > 0){
            position = new Vector3(1f, 0, 0);
            transform.Rotate(new Vector3(0, 0, 270));
        }
    }

    public void SetDirection(Vector2 dir){
        direction = dir;
    }

      public void SetSwordUser(GameObject user){
        swordUser = user;
    }
}
