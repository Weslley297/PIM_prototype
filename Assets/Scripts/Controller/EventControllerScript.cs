using UnityEngine;

public class EventControllerScript : MonoBehaviour
{
    private LightControllerScript lightController;
    private SoundControllerScript soundController;
    private EnemyControllerScript enemyController;
    private DialogControllerScript dialogController;
    private PlayerLightScript playerLight;
    private PlayerInputScript playerInput;
    private PlayerMovementScript playerMovement;
    private float moveSpeed;

    void Start()
    {
        lightController = GetComponent<LightControllerScript>();
        soundController = GetComponent<SoundControllerScript>();
        enemyController = GetComponent<EnemyControllerScript>();
        dialogController = GetComponent<DialogControllerScript>();

        var player = GameObject.FindGameObjectWithTag("Player");
        playerLight = player.GetComponent<PlayerLightScript>();
        playerInput = player.GetComponent<PlayerInputScript>();
        playerMovement = player.GetComponent<PlayerMovementScript>();
    }

    public void EventEmit(string name){
        if(string.IsNullOrEmpty(name)){
            return;
        }
        if(name.Equals("LabEnterEvent")){
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
        if(name.Equals("WalkToHologramEvent")){
            WalkToHologramEvent();
        }
        if(name.Equals("ListemTheHologramEvent")){
            ListemTheHologramEvent();
        }
    }

    private void LabEnterEvent(){
        playerLight.InactiveLights();
        lightController.SetGlobalLightIntensity(0.6f);
    }

    private void LabAutoDestructionEvent(){
        playerLight.InactiveLights();
        
        lightController.SetGlobalLightIntensity(0.4f);
        lightController.SetLightsColor(Color.red);
        lightController.SetInnerLightsColor(Color.white);
        lightController.activeIntermittence();
        soundController.PlayBattleMusic();

        enemyController.CreateBabyAranhaOn(new Vector2(4f, 80f));
        enemyController.CreateBabyAranhaOn(new Vector2(13f, 86f));
        enemyController.CreateBabyAranhaOn(new Vector2(17f, 79f));

        enemyController.CreateAranhaOn(new Vector2(74f, 47f));
        
        enemyController.CreateBabyAranhaOn(new Vector2(17f, 55f));
        enemyController.CreateBabyAranhaOn(new Vector2(20f, 48f));
        enemyController.CreateBabyAranhaOn(new Vector2(15f, 50f));
        enemyController.CreateBabyAranhaOn(new Vector2(8f, 52f));
        enemyController.CreateBabyAranhaOn(new Vector2(4f, 46f));
        enemyController.CreateBabyAranhaOn(new Vector2(0f, 51f));

        enemyController.CreateBabyAranhaOn(new Vector2(-43f, 68f));
        enemyController.CreateBabyAranhaOn(new Vector2(-57f, 61f));
        enemyController.CreateBabyAranhaOn(new Vector2(-56f, 69f));
        enemyController.CreateAranhaOn(new Vector2(-50f, 69));
        
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

    private void WalkToHologramEvent(){
        playerInput.InputDisable();
        moveSpeed = playerMovement.moveSpeed;
        playerMovement.SetMovement(new Vector2(-1, 0));
        playerMovement.SetMoveSpeed(2f);
    }

    private void ListemTheHologramEvent(){
        playerMovement.SetMovement(new Vector2(0, 1));
        playerInput.InputDisable();
        playerMovement.StopByTime(1);
        dialogController.InitLabDialog();
        playerMovement.SetMoveSpeed(moveSpeed);
    }
}
