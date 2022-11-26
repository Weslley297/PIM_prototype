using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBridgeScript : MonoBehaviour
{
    public float innerTime;
    private List<FadeOutEffectScript> brickFade = new List<FadeOutEffectScript>();
    private BoxCollider2D boxCollider;
    private float timer;
    private int index = 0;
    private bool actived;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        foreach (Transform child in transform)
        {
            brickFade.Add(child.gameObject.
                GetComponent<FadeOutEffectScript>());
        }
    }

    void Update()
    {
        if(!actived){
            return;
        }

        timer += Time.deltaTime;
        if(timer <= innerTime){
            return;
        }
       
        if(index >= brickFade.Count){
            actived = false;
            boxCollider.enabled = true;
            return;
        }

        timer = 0;
        brickFade[index].Activate();
        index++;
    }

    public void Active(){
        actived = true;
        index = 0;
        timer = 0;
    }
}
