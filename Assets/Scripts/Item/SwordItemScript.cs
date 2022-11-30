using UnityEngine;

public class SwordItemScript : MonoBehaviour
{
    public GameObject sword;
    public GameObject door;
    public GameObject holograma;


    private void Start() {
        
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag.Equals("Player")){
            GetSword(collider);
        }
    }

    private void GetSword(Collider2D collider){
        collider.gameObject.GetComponent<PlayerAttackScript>().SetSword(sword);
        collider.gameObject.GetComponent<PlayerInputScript>().DoAttack();

        door.GetComponent<DoorScript>().OpenDoor();
        holograma.GetComponent<FadeInEffectScript>().Activate();
        Destroy(gameObject);
    }
}
