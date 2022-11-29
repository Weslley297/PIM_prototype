
using UnityEngine;
using UnityEngine.UI;

public class CloseCurtainScript : MonoBehaviour
{
    private float transparence;
    private Image image;
    private bool active;
    private float speed;
    void Start()
    {
        image = GetComponent<Image>();
        transparence = image.color.a;
    }

    void Update()
    {
        if(!active){
            return;
        }

        if(transparence > 1 || transparence < 0){
            active = false;
        }

        transparence += speed;
        image.color = new Color(0, 0, 0, transparence); 
    }

    public void Close(float value){
        speed = value;
        transparence = 0;
        active = true;
    }

    public void Open(float value){
        speed = -value;
        transparence = 1;
        active = true;
    }
}
