using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLightScript : MonoBehaviour
{
    private Light2D light;
    void Start()
    {
        light = GameObject.FindGameObjectWithTag("PlayerLight").GetComponent<Light2D>();
    }

    public void SetLightIntensity(float value){
        light.intensity = value;
    }
    public void InactiveLights(){
        light.enabled = false;
    }

    public void ActiveLights(){
        light.enabled = false;
    }
}
