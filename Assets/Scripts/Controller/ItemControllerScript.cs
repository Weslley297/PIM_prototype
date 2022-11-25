using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControllerScript : MonoBehaviour
{
    private List<KeyScript> keys;
    private SoundControllerScript soundController;

    void Start()
    {
        soundController = GetComponent<SoundControllerScript>();
        keys = new List<KeyScript>();
    }

    public void AddNewKey(GameObject key){
        soundController.PlayCollectItemSound();
        keys.Add(key.GetComponent<KeyScript>());
    }

    public void CatchLifeItem(){
        soundController.PlayCollectLifeItemSound();
    }
    public bool UseTheKey(GameObject door){
        var index = keys.FindIndex(key => GameObject.ReferenceEquals(door, key.door));
        if(index == -1){
            return false;
        }

        keys.RemoveAt(index);
        return true;
    }
}
