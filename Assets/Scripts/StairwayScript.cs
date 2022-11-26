
using UnityEngine;

public class StairwayScript : MonoBehaviour
{
    public GameObject exit;
    public Vector2 direction;
    public float topCameraLimit, bottomCameraLimit, rightCameraLimit, leftCameraLimit;

    private GameObject player;
    private PlayerMovementScript playerMovement;
    private PlayerInputScript playerInput;
    private StairwayScript exitScript;
     private GameObject mainCamera;
    private bool active;
    private float timer;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        exitScript = exit.GetComponent<StairwayScript>();

        GetComponent<SpriteRenderer>().sprite = getSprite();
    }

    private Sprite getSprite(){
        return null;
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "Player"){  
            player = collider.gameObject;
            playerMovement = player.GetComponent<PlayerMovementScript>();
            playerInput = player.GetComponent<PlayerInputScript>();

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
        playerInput.InputEnable();
        exit.SetActive(true);
        active = false;
    }

    private void PlayerStairsEnter(){
        playerInput.InputDisable();
        playerMovement.SetMovement(direction);
        playerMovement.SetMoveSpeed(1f);
    }

    private void PlayerStairsExit(){
        if(!exit.activeSelf){
            return;
        }

        exit.SetActive(false);
        setLimitsFromCamera();
        player.transform.position = getPlayerPosition();
        playerMovement.SetMovement(-exitScript.direction); 
    } 

    private void setLimitsFromCamera(){
        //mainCamera.GetComponent<CameraAudioScript>().PlayBattleSound();
        mainCamera.GetComponent<CameraScript>()
            .setCameraLimits(
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
