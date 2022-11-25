using UnityEngine;

public class DestructibleScript : MonoBehaviour
{
    public float HP;
    public float delay;
    private SpriteRenderer sprite;
    private bool colliding;
    private bool active;
    private float timer;


    private void Start() {
        sprite = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(colliding){
            return;
        }

        if(!collision.collider.tag.Equals("Sword")){
            return;
        }

        colliding = true;
        active = true;

        sprite.enabled = false;
        transform.GetChild(0).gameObject
            .GetComponent<FadeOutEffectScript>()
            .Activate();

        transform.GetChild(1).gameObject
            .GetComponent<FadeOutEffectScript>()
            .Activate();
    }

    private void Update() {
        if(!active){
            return;
        }

        timer += Time.deltaTime;
        if(timer >= delay){
            Destroy(gameObject);
        }
    }

}
