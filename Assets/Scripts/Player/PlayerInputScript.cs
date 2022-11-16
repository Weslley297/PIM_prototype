using UnityEngine;

public class PlayerInputScript : MonoBehaviour
{

    private PlayerMovementScript playerMovement;
    private PlayerAttackScript playerAttack;
    private PlayerAudioScript playerAudio;
    private Vector2 movement;
    private float inputHorizontal;
    private float inputVertical;
    private float timer;
    private bool inputDisabled;
    void Start()
    {
        playerMovement = GetComponent<PlayerMovementScript>();
        playerAttack = GetComponent<PlayerAttackScript>();
        playerAudio = GetComponent<PlayerAudioScript>();
    }

    void Update()
    {
        if(inputDisabled){
            return;
        }

        if(timer < 0){
            timer += Time.deltaTime;
            return;
        }

        if (Input.GetButtonDown("Attack")){
            playerAttack.SwordAttack();
            playerAudio.PlayAttackSound();
            timer = -playerAttack.GetAttackDelay();
            playerMovement.StopByTime(timer);            
            return;
        }

        movement = new Vector2(0, 0);
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        if(inputHorizontal != 0){
            movement = new Vector2(inputHorizontal, 0);
            playerMovement.SetMovement(movement);
            playerAudio.PlayWalkSound();
            return;
        }

        inputVertical = Input.GetAxisRaw("Vertical");
        if(inputVertical != 0){
            movement = new Vector2(0, inputVertical);
            playerMovement.SetMovement(movement);
            playerAudio.PlayWalkSound();
            return;
        }
        
        playerAudio.StopWalkSound();
        playerMovement.SetMovement(movement);
    }

    public void InputDisableByTime(float time){
        timer = -time;
    }

    public void InputDisable(){
        inputDisabled = true;
    }
    
    public void InputEnable(){
        inputDisabled = false;
    }
}
