
using UnityEngine;

public class StairwayScript : MonoBehaviour
{
    public GameObject exit;
    public Vector2 direction;
    public Sprite topSprite, bottomSprite, leftSprite, rightSprite;
    public float topCameraLimit, bottomCameraLimit, rightCameraLimit, leftCameraLimit;

    private GameObject player;
    private PlayerMovement playerMovement;
    private StairwayScript exitScript;
     private MainCameraScript cameraScript;
    private bool active;
    private float timer;

    void Start()
    {
        cameraScript = GameObject.FindGameObjectWithTag("MainCamera")
            .GetComponent<MainCameraScript>();

        exitScript = exit.GetComponent<StairwayScript>();

        GetComponent<SpriteRenderer>().sprite = getSprite();
    }

    private Sprite getSprite(){
        return direction.y > 0 ? topSprite
            : direction.y < 0 ? bottomSprite
            : direction.x > 0 ? rightSprite
            : direction.x < 0 ? leftSprite: null;
    }

    private void OnTriggerEnter2D(Collider2D collider) {
       if(collider.tag == "Player"){  
            player = collider.gameObject;
            playerMovement = player.GetComponent<PlayerMovement>();
            
            timer = -1.5f;
            active = true;
        }
    }    

    private void Update() {
        if(!active){
            return;
        }

        if(timer <= 0){
            timer += Time.deltaTime;
        }

        if(timer < -0.75){
            PlayerStairsEnter();
            return;
        }

        if(timer < 0){
            PlayerStairsExit();
            return;
        }

        playerMovement.SetMoveSpeed(6);
        playerMovement.InputEnabled();
        exit.SetActive(true);
        active = false;
    }

    private void PlayerStairsEnter(){
        playerMovement.InputDisabled();
        playerMovement.SetMoviment(direction);
        playerMovement.SetMoveSpeed(1);
    }

    private void PlayerStairsExit(){
        if(!exit.activeSelf){
            return;
        }

        exit.SetActive(false);
        setLimitsFromCamera();
        player.transform.position = getPlayerPosition();
        playerMovement.SetMoviment(-exitScript.direction); 
    } 

    private void setLimitsFromCamera(){
        cameraScript.setCameraLimits(
            exitScript.topCameraLimit, 
            exitScript.bottomCameraLimit, 
            exitScript.leftCameraLimit, 
            exitScript.rightCameraLimit
        );
    }

    private Vector3 getPlayerPosition(){
        Vector3 offset = new Vector2(0, 0.6f);
        Vector3 exitPosition = -exitScript.direction;
        return exit.transform.position + exitPosition + offset;
    }
}
