using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    private GameObject player;
    private PlayerScript playerScript;
    private PlayerAttackScript playerAttackScript;
    private List<GameObject> itens;
    private Vector3 lastSavedPosition;
    private float lastSavedLife;
    private bool haventASword;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
        playerAttackScript = player.GetComponent<PlayerAttackScript>();
    }
    public void Save(){
        lastSavedPosition = player.transform.position;
        lastSavedLife = playerScript.HP;

        haventASword = playerAttackScript.NotHaveASword();
    }
    
    public void Load(){
        player.transform.position = lastSavedPosition;
        playerScript.HP = lastSavedLife;

        if(haventASword){
            player.GetComponent<PlayerAttackScript>().SetSword(null);
        }

        foreach (var item in itens)
        {
            item.SetActive(true);
        }
    }
}
