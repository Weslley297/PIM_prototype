using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLightScript : MonoBehaviour
{
    private Light2D playerlight;
    void Start()
    {
        playerlight = GameObject.FindGameObjectWithTag("PlayerLight").GetComponent<Light2D>();
    }

    public void SetLightIntensity(float value){
        playerlight.intensity = value;
    }
    public void InactiveLights(){
        playerlight.enabled = false;
    }

    public void ActiveLights(){
        playerlight.enabled = false;
    }
}
