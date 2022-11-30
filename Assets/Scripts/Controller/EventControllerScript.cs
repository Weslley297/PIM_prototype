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
    private PlayerAttackScript playerAttack;
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
        playerAttack = player.GetComponent<PlayerAttackScript>();

        curtainScript.Open(0.01f);
    }

    public void EventEmit(string name){
        if(string.IsNullOrEmpty(name)){
            return;
        }
        if(name.Equals("GetUpEvent")){
            GetUpEvent();
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
        if(name.Equals("DoorDialogEvent")){
            DoorDialogEvent();
        }
        if(name.Equals("LockedDoorDialogEvent")){
            LockedDoorDialogEvent();
        }
        if(name.Equals("SolarDoorDialogEvent")){
            SolarDoorDialogEvent();
        }
        if(name.Equals("DestructibleRockDialogEvent")){
            DestructibleRockDialogEvent();
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

        if(name.Equals("DeathEvent")){
            Death();
        }
        if(name.Equals("GameOverEvent")){
            GameOver();
        }
    }

    private void GetUpEvent(){
        playerInput.InputDisableByTime(1);
        playerMovement.StopByTime(1);
        dialogController.InitGetUpDialog();
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

        playerInput.InputDisableByTime(2);
        playerMovement.StopByTime(2);
        dialogController.InitLabAutoDestructionDialog();
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

        playerInput.InputDisableByTime(1);
        playerMovement.StopByTime(1);
        dialogController.InitBridgeOutDialog();
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

    private void DoorDialogEvent(){
        if(!playerAttack.NotHaveASword()){
            return;
        }
        
        playerMovement.SetMovement(new Vector2(-1, 0));
        playerInput.InputDisableByTime(1);
        playerMovement.StopByTime(1);
        dialogController.InitDoorDialog();
    }

    private void DestructibleRockDialogEvent(){
        if(!playerAttack.NotHaveASword()){
            return;
        }
        
        playerInput.InputDisableByTime(1);
        playerMovement.StopByTime(1);
        dialogController.InitDestructibleRockDialog();
    }

    private void LockedDoorDialogEvent(){
        playerInput.InputDisableByTime(1);
        playerMovement.StopByTime(1);
        dialogController.InitLockedDoorDialog();
    }

    private void SolarDoorDialogEvent(){
        playerInput.InputDisableByTime(1);
        playerMovement.StopByTime(1);
        dialogController.InitSolarLockedDoorDialog();
    }

    private void LabExitEvent(){
        WalkUp();
        curtainScript.Close(0.01f);
    }

    private void EndGame(){
        soundController.StopMusic();
        SceneManager.LoadScene(sceneNameOnEnd, LoadSceneMode.Single);
    }

    private void Death(){
        curtainScript.Close(0.005f);
    }

    private void GameOver(){
        soundController.StopMusic();
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
