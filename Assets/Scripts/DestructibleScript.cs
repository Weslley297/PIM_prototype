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

    public void Destruct(){
        active = true;
        sprite.enabled = false;

        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<FadeOutEffectScript>()
                .Activate();
        }
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
