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

    public void CreateLabEnemies(){
        CreateBabyAranhaOn(new Vector2(4f, 84f));
        CreateBabyAranhaOn(new Vector2(13f, 86f));
        CreateBabyAranhaOn(new Vector2(17f, 79f));

        CreateAranhaOn(new Vector2(74.5f, 46f));
    
        CreateBabyAranhaOn(new Vector2(17f, 55f));
        CreateBabyAranhaOn(new Vector2(20f, 48f));
        CreateBabyAranhaOn(new Vector2(15f, 50f));
        CreateBabyAranhaOn(new Vector2(6f, 52f));
        CreateBabyAranhaOn(new Vector2(4f, 46f));
        CreateBabyAranhaOn(new Vector2(0f, 51f));

        CreateBabyAranhaOn(new Vector2(-43f, 68f));
        CreateBabyAranhaOn(new Vector2(-57f, 61f));
        CreateBabyAranhaOn(new Vector2(-56f, 69f));
        CreateAranhaOn(new Vector2(-50f, 69));
    }

    public void ClearEnemy(){
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}
