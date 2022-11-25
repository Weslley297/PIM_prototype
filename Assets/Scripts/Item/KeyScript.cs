using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public GameObject door;

    private ItemControllerScript itemController;
    private bool colliding;

    void Start()
    {
        itemController = GameObject.Find("GameController").GetComponent<ItemControllerScript>();
    }
    
    void OnTriggerEnter2D(Collider2D collider) {
        if(colliding){
            return;
        }

        if(collider.tag.Equals("Player")){
            colliding = true;
            itemController.AddNewKey(gameObject);
            Destroy(gameObject);
        }

        colliding = false;
    }
}
