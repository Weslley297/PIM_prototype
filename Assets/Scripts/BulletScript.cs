using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float moveSpeed;
    public float damage; 

    private Rigidbody2D rb;
    private Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        Destroy(gameObject);
        
        if(collider.tag == "Enemy"){
            collider.gameObject
                .GetComponent<EnemyAI>()
                .TakeDamage(damage, direction);
        }

        if(collider.tag == "Player"){            
            collider.gameObject
                .GetComponent<PlayerMovement>()
                .AddImpulse(direction);
        }
    }
    

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
    }

    public void SetDirection(Vector2 dir){
        direction = dir;
    }
}
