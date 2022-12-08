using UnityEngine;

public class DestructibleScript : MonoBehaviour
{
    public float HP;
    public float delay;
    public GameObject destructionParticle;

    private SpriteRenderer sprite;
    private AudioSource audioSource;
    private bool colliding;
    private bool active;
    private float timer;


    private void Start() {
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(colliding){
            return;
        }
        if(!collider.tag.Equals("Sword")){
            return;
        }

        audioSource.Play();
        if(destructionParticle != null)
        {
            Instantiate(destructionParticle, this.transform);
        }
        colliding = true;
        Destruct();
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
