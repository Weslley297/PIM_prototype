using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconBlinkScript : MonoBehaviour
{
    public float interval;

    private Image image;
    private bool actived;
    private float timer;
    void Start()
    {
        image = gameObject.GetComponent<Image>();
    }

    void Update()
    {
        if(!actived){
            return;
        }

        timer += Time.deltaTime;
        if(timer < interval){
            return;
        }

        timer = 0;
        image.enabled = !image.enabled;
    }

    public void Active(){
        image.enabled = true;
        actived = true;
        timer = 0;
    }

    public void Inactive(){
        image.enabled = false;
        actived = false;
        timer = 0;
    }
}
