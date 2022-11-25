using UnityEngine;

public class FadeOutEffectScript : MonoBehaviour
{
    public Vector2 direction;
    public float transparenceSpeed;
    public float delay;

    private SpriteRenderer sprite;
    private float tranparence;
    private bool active;
    private float timer;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        tranparence = sprite.color.a;
    }

    void Update()
    {
        if(!active){
            return;
        }

        FadeOut();

        timer += Time.deltaTime;
        if(timer >= delay){
            Destroy(gameObject);
        }
    }

    private void FadeOut(){
        transform.position += (Vector3)direction;

        tranparence -= transparenceSpeed;
        sprite.color = new Color(255, 255, 255, tranparence);
    }

    public void Activate(){
        active = true;
    }
}
