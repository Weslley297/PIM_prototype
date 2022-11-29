using UnityEngine;
using TMPro;

public class DialogScript : MonoBehaviour
{
    public float typingSpeed;

    private RectTransform rectTransform;
    private AudioSource audioSource;
    private IconBlinkScript iconBlink;
    private TextMeshProUGUI textMesh;
    private string fullText = "";
    private bool writefinish = true;
    private float timer;
    private int index;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rectTransform = GetComponent<RectTransform>();
        iconBlink = GameObject.Find("DialogIcon").GetComponent<IconBlinkScript>();
        textMesh = GameObject.Find("DialogText").GetComponent<TextMeshProUGUI>();

        Hide();
    }

    void FixedUpdate()
    {
        if(writefinish){
            return;
        }

        timer += Time.deltaTime;
        if(timer < typingSpeed){
            return;
        }

        timer = 0;
        if(textMesh.text.Length == fullText.Length){
            setWriteFinish();
            return;
        }

        textMesh.text += fullText[index];
        index++;
    }

    public void setWriteFinish(){
        audioSource.Stop();
        textMesh.text = fullText;
        index = fullText.Length;
        iconBlink.Active();
        writefinish = true;
    }

    public void SetText(string text){
        audioSource.Play();
        fullText = text;
        textMesh.text = "";
        index = 0;
        iconBlink.Inactive();
        writefinish = false;
    }

    public bool isFinished(){
        return writefinish;
    }

    public void Show(){
        rectTransform.localScale = new Vector3(1, 1, 1);
    }

    public void Hide(){
        rectTransform.localScale = new Vector3(0, 0, 0);
    }
}
