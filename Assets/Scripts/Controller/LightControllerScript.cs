using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;
using System.Linq;

public class LightControllerScript : MonoBehaviour
{
    private Light2D globalLight;
    private List<Light2D> lights = new List<Light2D>();
    private List<Light2D> innerlights = new List<Light2D>();
    private List<LightEffectScript> lightsEffects = new List<LightEffectScript>();

    void Start()
    {
        globalLight = GameObject.Find("GlobalLight").GetComponent<Light2D>();
        var dinamicLights = GameObject.FindGameObjectsWithTag("DinamicLight");
        foreach(var dinamicLight in dinamicLights)
        {
            lights.Add(dinamicLight.GetComponent<Light2D>());
            lightsEffects.Add(dinamicLight.GetComponent<LightEffectScript>());
            
            if(dinamicLight.transform.childCount <= 0){
                continue;
            }

            var inner = dinamicLight.transform.GetChild(0).gameObject;
            innerlights.Add(inner.GetComponent<Light2D>());
            lightsEffects.Add(inner.GetComponent<LightEffectScript>());
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

    public void SetInnerLightsIntensity(float value){
        foreach (var innerlight in innerlights)
        {
            innerlight.intensity = value;
        }
    }

    public void SetInnerLightsColor(Color color){
        foreach (var innerlight in innerlights)
        {
            innerlight.color = color;
        }
    }

    public void activeIntermittence (){
        foreach (var light in lightsEffects)
        {
            light.ActiveIntermitence();

        }
    }
    
}
