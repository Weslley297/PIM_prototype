using System.Collections.Generic;
using UnityEngine;

public class DialogControllerScript : MonoBehaviour
{
    private List<string> dialog = new List<string>();
    private DialogScript dialogScript;
    private PlayerInputScript playerInput;
    private bool actived;
    private float timer;
    private int index;
    void Start()
    {
        dialogScript = GameObject.Find("DialogBox").GetComponent<DialogScript>();

        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputScript>();
    }

    void Update()
    {
        if(!actived){
            return;
        }

        if (Input.anyKeyDown)
        {
            DialogInteract();
        }
    }

    private void DialogInteract (){
        if(!dialogScript.isFinished()){
            dialogScript.setWriteFinish();
            return;
        }

        if(index == dialog.Count){
            dialogScript.Hide();
            playerInput.InputEnable();
            return;
        }

        dialogScript.SetText(dialog[index]);
        index++;
    }

    public void InitLabDialog(){
        dialog.Add("isso é um teste grande");
        dialog.Add("Este é o segundo teste um pouco bem maior");

        Active();
        DialogInteract();
    }

    private void Active(){
        dialogScript.Show();
        actived = true;
        index = 0;
    }

    public void Inactive(){
        dialog.Clear();
        dialogScript.Hide();
        actived = false;
    }
}
