using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerScript : MonoBehaviour
{
    public GameObject aranha;
    public GameObject babyAranha;
    
    public void CreateAranhaOn(Vector3 position){
        Instantiate(aranha, position, Quaternion.identity);
    }
    public void CreateBabyAranhaOn(Vector3 position){
        Instantiate(babyAranha, position, Quaternion.identity);
    }
}
