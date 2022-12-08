using System.Collections.Generic;
using UnityEngine;

public class DialogControllerScript : MonoBehaviour
{
    public GameObject NPC;
    private List<string> dialog = new List<string>();
    private DialogScript dialogScript;
    private PlayerInputScript playerInput;
    private bool hasNPC;
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
            EndDialog();
            return;
        }

        dialogScript.SetText(dialog[index]);
        index++;
    }

    private void EndDialog(){
        dialogScript.Hide();
        playerInput.InputEnable();
        actived = false;

        if(!hasNPC){
            return;
        }

        NPC.GetComponent<FadeOutEffectScript>().Activate();
    }

    public void InitLabDialog(){
        dialog.Clear();
        //dialog.Add("Diário pessoal, dia 78 após o incidente");
        dialog.Add("Personal diary, day 78 after the incident");
        //dialog.Add("O mundo de maravilhas que idealizei com a utilização da solarita é uma farsa, a Corenetic causou imenso impacto em nosso planeta, eu sou um tolo! ");
        dialog.Add("The utopic world of wonders I envisioned using solarite is a sham, Corenetic has made such an impact on our planet, I am a fool!");
        //dialog.Add("Após inúmeros dias neste inferno, finalmente criei um protótipo de arma que pode me ajudar a sair daqui, porém, não sei se mereço mais uma chance depois de tudo causei… ");
        dialog.Add("After countless days in this hell, I have finally created a prototype weapon that can help me get out of here");
        dialog.Add("... but I don't know if I deserve another chance after all I have caused... ");
        //dialog.Add("………………………… trava de seguranç……………………………………… cuidado."); 
        dialog.Add(" War………………………… safety loc………………………………………arning .");

        Active();
        hasNPC = true;
        NPC.GetComponent<FadeInEffectScript>().Activate();
        dialogScript.SetPlaySound(true);
        DialogInteract();
    }

    public void InitDestructibleRockDialog(){
        //TipDialog("Essa pedra está quebrando, um golpe forte deve destruí-la.");
        TipDialog("This stone is weak, a strong strike should destroy it.");
        DialogInteract();
    }

    public void InitDoorDialog(){
        //TipDialog("Parece que a porta está emperrada, deve haver outra passagem.");
        TipDialog("It looks like the door is stuck, there must be another way through.");
        DialogInteract();
    }

    public void InitGetUpDialog(){
        //TipDialog("Acho que não me viram, preciso sair daqui.");
        TipDialog("I don't think they saw me, I need to get out of here.");
        DialogInteract();
    }

    public void InitLockedDoorDialog(){
        //TipDialog("Porta trancada, necessário cartão de acesso.");
        TipDialog("this door is locked, apparently an access card is required.");
        DialogInteract();
    }

    public void InitSolarLockedDoorDialog(){
        //TipDialog("Esta porta deve abrir com alguma fonte de energia.");
        TipDialog("This door is different, it seems like it need some outsource of energy to open.");
        DialogInteract();
    }

    public void InitBridgeOutDialog(){
        //TipDialog("Não consigo voltar por aqui, preciso achar outro caminho.");
        TipDialog("I can't go back, I must find another path.");
        DialogInteract();
    }

    public void InitLabAutoDestructionDialog(){
        //TipDialog("ATENÇÃO! ATENÇÃO! PROCESSO DE AUTO-DESTRUIÇÃO INICIADO, EVACUAR O LOCAL IMEDIATAMENTE!");
        TipDialog("ATTENTION! ATTENTION! SELF-DESTRUCT PROCESS INITIATED, EVACUATE THE SITE IMMEDIATELY!");
        DialogInteract();
    }

    public void TipDialog(string text){
        dialog.Clear();
        dialog.Add(text);

        Active();
        hasNPC = false;
        dialogScript.SetPlaySound(false);
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
