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
            actived = false;
            return;
        }

        dialogScript.SetText(dialog[index]);
        index++;
    }

    public void InitLabDialog(){
        dialog.Add("Diário pessoal, dia 78 após o incidente");
        dialog.Add("O mundo de maravilhas que idealizei com a utilização da solarita é uma farsa, a Corenetic causou imenso impacto em nosso planeta, eu sou um tolo! ");
        dialog.Add("Após inúmeros dias neste inferno, finalmente criei um protótipo de arma que pode me ajudar a sair daqui, porém, não sei se mereço mais uma chance depois de tudo causei… ");
        dialog.Add("&&&$$$$@@@@@ … trava de seguranç@@@@@@&&&&$$$$ ..cuidado.");

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
