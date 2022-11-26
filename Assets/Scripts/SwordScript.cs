using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public float damage;

    private GameObject swordUser;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Vector3 position;
    private Collider2D collider;
    private Vector2 colliderSize = new Vector2(1.3f, 1.3f);
    private Vector2 colliderOffset= new Vector2(0, 0.4f);
    private Vector2 direction;

    private float timer = 0;

    private void Start() {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        sprite.enabled = false;
        timer = -1f;

        SetSwordPosition();
    }

    void OnTriggerEnter2D(Collider2D otherCollider) 
    {
        if(otherCollider.tag == "Enemy"){
            otherCollider.gameObject
                .GetComponent<EnemyScript>()
                .TakeDamage(damage, direction);

            if(collider != null){
                collider.enabled = false;
            }
            
        }
    }

    void Update()
    {
        transform.position = swordUser.transform.position + position;
        if(timer < -0.4){
            timer += Time.deltaTime;
            return;
        }

        if(timer <= 0 && collider == null){
            CreateCollider();
            timer += Time.deltaTime;
            return;
        }

        if(timer > 0 && collider){
            Destroy(gameObject);
        }
    }

    private void CreateCollider(){
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        collider.size = colliderSize;
        collider.offset = colliderOffset;
    }

    private void SetSwordPosition(){
        if(direction.y < 0){
            position = new Vector3(0, -1.0f, 0);
            transform.Rotate(new Vector3(0, 0, 180f));
            sprite.sortingOrder = 6;
        }

        if(direction.y > 0){
            position = new Vector3(0, 1f, 0);
            transform.Rotate(new Vector3(0, 0, 0));
            sprite.sortingOrder = 4;
        }

        if(direction.x < 0){
            position = new Vector3(-1, 0, 0);
            transform.Rotate(new Vector3(0, 0, 270));
            sprite.sortingOrder = 5;
        }

        if(direction.x > 0){
            position = new Vector3(1f, 0, 0);
            transform.Rotate(new Vector3(0, 0, 270));
            sprite.sortingOrder = 5;
        }
    }

    public void SetDirection(Vector2 dir){
        direction = dir;
    }

      public void SetSwordUser(GameObject user){
        swordUser = user;
    }
}
