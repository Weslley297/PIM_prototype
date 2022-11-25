using UnityEngine;

public class SwordItemScript : MonoBehaviour
{
    public GameObject sword;
    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag.Equals("Player")){
            collider.gameObject.GetComponent<PlayerAttackScript>().SetSword(sword);
            collider.gameObject.GetComponent<PlayerInputScript>().DoAttack();
            Destroy(gameObject);
        }
    }
}
