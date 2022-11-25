using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightEffectScript : MonoBehaviour
{
    public float maxRadius, minRadius;
    public float speed;
    private Light2D light2D;
    private bool intermittenceGrowing;
    private bool intermittenceActivated;
    void Start()
    {
        light2D = GetComponent<Light2D>();
    }

    void Update()
    {
        if(intermittenceActivated){
            Intermitence();
        }
    }

    private void Intermitence(){
        if(light2D.pointLightOuterRadius < minRadius 
            || light2D.pointLightOuterRadius > maxRadius){
            intermittenceGrowing = !intermittenceGrowing;
        }

        float variation = intermittenceGrowing ? speed : -speed;
        light2D.pointLightOuterRadius += variation;
    }

    public void ActiveIntermitence(){
        intermittenceActivated = true;
    }
}
