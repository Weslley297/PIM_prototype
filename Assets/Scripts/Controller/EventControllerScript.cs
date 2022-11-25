using UnityEngine;

public class EventControllerScript : MonoBehaviour
{
    private LightControllerScript lightController;
    private SoundControllerScript soundController;
    private EnemyControllerScript enemyController;
    private GameObject player;

    void Start()
    {
        lightController = GetComponent<LightControllerScript>();
        soundController = GetComponent<SoundControllerScript>();
        enemyController = GetComponent<EnemyControllerScript>();

        player = GameObject.FindGameObjectWithTag("Player");
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
    }

    private void LabEnterEvent(){
        player.GetComponent<PlayerLightScript>().InactiveLights();
        lightController.SetGlobalLightIntensity(0.75f);
        soundController.PlayLabMusic();
    }

    private void LabAutoDestructionEvent(){
        player.GetComponent<PlayerLightScript>().InactiveLights();
        
        lightController.SetGlobalLightIntensity(0.2f);
        lightController.SetLightsColor(Color.red);
        lightController.activeIntermittence();
        soundController.PlayBattleMusic();

        // enemyController.CreateAranhaOn(new Vector2(21f, 50f));
    }
}
