using UnityEngine;

public class EventControllerScript : MonoBehaviour
{
    private LightControllerScript lightController;
    private SoundControllerScript soundController;
    private EnemyControllerScript enemyController;
    private PlayerLightScript playerLight;
    private PlayerInputScript playerInput;
    private PlayerMovementScript playerMovement;
    private float moveSpeed;

    void Start()
    {
        lightController = GetComponent<LightControllerScript>();
        soundController = GetComponent<SoundControllerScript>();
        enemyController = GetComponent<EnemyControllerScript>();

        var player = GameObject.FindGameObjectWithTag("Player");
        playerLight = player.GetComponent<PlayerLightScript>();
        playerInput = player.GetComponent<PlayerInputScript>();
        playerMovement = player.GetComponent<PlayerMovementScript>();
    }

    public void EventEmit(string name){
        if(string.IsNullOrEmpty(name)){
            return;
        }

        if(name.Equals("LabEnter")){
            LabEnterEvent();
        }

        if(name.Equals("LabAutoDestructionEvent")){
            LabAutoDestructionEvent();
        }

        if(name.Equals("BridgeCrossEvent")){
            BridgeCrossEvent();
        }

        if(name.Equals("BridgeOutEvent")){
            BridgeOutEvent();
        }
    }

    private void LabEnterEvent(){
        playerLight.InactiveLights();
        lightController.SetGlobalLightIntensity(0.75f);
        soundController.PlayLabMusic();
    }

    private void LabAutoDestructionEvent(){
        playerLight.InactiveLights();
        
        lightController.SetGlobalLightIntensity(0.2f);
        lightController.SetLightsColor(Color.red);
        lightController.activeIntermittence();
        soundController.PlayBattleMusic();

        enemyController.CreateAranhaOn(new Vector2(21f, 50f));
    }

    private void BridgeCrossEvent(){
        moveSpeed = playerMovement.moveSpeed;
        playerInput.InputDisable();
        playerMovement.SetMovement(new Vector2(0, 1));
        playerMovement.SetMoveSpeed(1f);

        GameObject.Find("EarthBridge").gameObject
            .GetComponent<TrapBridgeScript>()
            .Active();
    }

    private void BridgeOutEvent(){
        playerInput.InputEnable();
        playerMovement.SetMoveSpeed(moveSpeed);
    }
}
