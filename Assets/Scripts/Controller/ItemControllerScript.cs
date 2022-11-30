using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControllerScript : MonoBehaviour
{
    private List<KeyScript> keys;
    private List<GameObject> CollectedItens;

    void Start()
    {
        keys = new List<KeyScript>();
    }

    public void AddNewKey(GameObject key)
    {
        keys.Add(key.GetComponent<KeyScript>());
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
