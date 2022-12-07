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
            DoAttack();        
            return;
        }

        movement = new Vector2(0, 0);
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        if(inputHorizontal != 0){
            DoHorizontalMovement(inputHorizontal);
            return;
        }

        inputVertical = Input.GetAxisRaw("Vertical");
        if(inputVertical != 0){
            DoVerticalMovement(inputVertical);
            return;
        }
        
        playerAudio.StopWalkSound();
        playerMovement.SetMovement(movement);
    }

    public void DoAttack(){
        if(playerAttack.NotHaveASword() || playerAttack.IsAttacking()){
            return;
        }

        playerAttack.SwordAttack();
        playerAudio.PlayAttackSound();
        timer = -playerAttack.GetAttackDelay();
        playerMovement.StopByTime(timer);  
    }

    private void DoHorizontalMovement(float input){
        movement = new Vector2(input, 0);
        playerMovement.SetMovement(movement);
        playerAudio.PlayWalkSound();
    }

    private void DoVerticalMovement(float input){
        movement = new Vector2(0, input);
        playerMovement.SetMovement(movement);
        playerAudio.PlayWalkSound();
    }

    public void InputDisableByTime(float time){
        timer = -time;
        playerAudio.StopSound();
    }

    public void InputDisable(){
        inputDisabled = true;
        playerAudio.StopSound();
    }
    
    public void InputEnable(){
        inputDisabled = false;
    }
}
