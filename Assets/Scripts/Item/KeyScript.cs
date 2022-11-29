using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public GameObject door;

    private AudioSource audioSource;
    private ItemControllerScript itemController;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private bool colliding;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        itemController = GameObject.Find("GameController").GetComponent<ItemControllerScript>();
    }
    
    void OnTriggerEnter2D(Collider2D collider) {
        if(colliding){
            return;
        }

        if(collider.tag.Equals("Player")){
            colliding = true;
            GetItem(collider);
        }
    }

    private void GetItem(Collider2D collider){
        itemController.AddNewKey(gameObject);
        boxCollider.enabled = false;
        spriteRenderer.enabled = false;
        audioSource.Play();

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
