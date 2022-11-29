using UnityEngine;
using UnityEngine.SceneManagement;

public class EventControllerScript : MonoBehaviour
{
    public string sceneNameOnEnd;
    private LightControllerScript lightController;
    private SoundControllerScript soundController;
    private EnemyControllerScript enemyController;
    private DialogControllerScript dialogController;
    private PlayerLightScript playerLight;
    private PlayerInputScript playerInput;
    private PlayerMovementScript playerMovement;
    private CloseCurtainScript curtainScript;
    private float moveSpeed;

    void Start()
    {
        curtainScript = GameObject.Find("Curtain").GetComponent<CloseCurtainScript>();

        lightController = GetComponent<LightControllerScript>();
        soundController = GetComponent<SoundControllerScript>();
        enemyController = GetComponent<EnemyControllerScript>();
        dialogController = GetComponent<DialogControllerScript>();

        var player = GameObject.FindGameObjectWithTag("Player");
        playerLight = player.GetComponent<PlayerLightScript>();
        playerInput = player.GetComponent<PlayerInputScript>();
        playerMovement = player.GetComponent<PlayerMovementScript>();

        curtainScript.Open(0.01f);
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
        if(name.Equals("WalkToHologramEvent")){
            WalkToHologramEvent();
        }
        if(name.Equals("ListemTheHologramEvent")){
            ListemTheHologramEvent();
        }
        if(name.Equals("BridgeCrossEvent")){
            BridgeCrossEvent();
        }
        if(name.Equals("BridgeOutEvent")){
            BridgeOutEvent();
        }
        if(name.Equals("LabExitEvent")){
            LabExitEvent();
        }
        if(name.Equals("EndGameEvent")){
            EndGame();
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
        enemyController.CreateLabEnemies();   
    }

    private void BridgeCrossEvent(){
        WalkUp();

        GameObject.Find("EarthBridge").gameObject
            .GetComponent<TrapBridgeScript>()
            .Active();
    }

    private void WalkUp(){
        moveSpeed = playerMovement.moveSpeed;
        playerInput.InputDisable();
        playerMovement.SetMovement(new Vector2(0, 1));
        playerMovement.SetMoveSpeed(1f);
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

    private void LabExitEvent(){
        WalkUp();
        curtainScript.Close(0.01f);
    }

    private void EndGame(){
        SceneManager.LoadScene(sceneNameOnEnd, LoadSceneMode.Single);
    }
}
