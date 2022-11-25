using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;
using System.Linq;

public class LightControllerScript : MonoBehaviour
{
    private Light2D globalLight;
    private List<Light2D> lights = new List<Light2D>();
    private List<LightEffectScript> lightsEffects = new List<LightEffectScript>();

    void Start()
    {
        globalLight = GameObject.Find("GlobalLight").GetComponent<Light2D>();
        var dinamicLights = GameObject.FindGameObjectsWithTag("DinamicLight");
        foreach(var dinamicLight in dinamicLights)
        {
            lights.Add(dinamicLight.GetComponent<Light2D>());
            lightsEffects.Add(dinamicLight.GetComponent<LightEffectScript>());
        }
    }

    public void SetGlobalLightIntensity(float value){
        globalLight.intensity = value;
    }

    public void SetGlobalLightColor(Color color){
        globalLight.color = color;
    }

    public void SetLightsIntensity(float value){
        foreach (var light in lights)
        {
            light.intensity = value;
        }
    }

    public void SetLightsColor(Color color){
        foreach (var light in lights)
        {
            light.color = color;
        }
    }

    public void activeIntermittence (){
        foreach (var light in lightsEffects)
        {
            light.ActiveIntermitence();

        }
    }
}
