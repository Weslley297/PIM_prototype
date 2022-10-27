using UnityEngine;

public class BulletMoviment : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D rb;
    private Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
    }

    public void SetDirection(Vector2 dir){
        direction = dir;
    }
}
