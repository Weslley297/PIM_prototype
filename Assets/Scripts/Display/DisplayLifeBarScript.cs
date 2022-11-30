using System;
using UnityEngine;

public class DisplayLifeBarScript : MonoBehaviour
{
    public GameObject fullHeartPrefab;
    public GameObject emptyHeartPrefab;

    private PlayerScript player;
    private float life;
    private float maxLife;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<PlayerScript>();
        maxLife = player.maxHP;
        life = player.HP;

        DrawLifeBar();
    }

    void Update()
    {
        if(life != player.HP){
            life = player.HP;
            CleanLifeBar();
            DrawLifeBar();
        } 
    }

    private void DrawLifeBar(){
        int index = 0;
        var fullHearts = life / 10;
        for (int i = 1; i <= fullHearts; i++)
        {
            CreateHeart(fullHeartPrefab, index);
            index++;
        }

        var emptyLife = maxLife - index * 10;
        var emptyHearts = (emptyLife) / 10;
        for (int i = 1; i <= emptyHearts; i++)
        {
            CreateHeart(emptyHeartPrefab, index);
            index++;
        }
    }

    private void CreateHeart(GameObject heart, int index){
        var heartObj = Instantiate(heart);
        heartObj.transform.SetParent(gameObject.transform);
        heartObj.transform.localScale = new Vector3(1, 1, 1);

        float line = (float)Math.Floor((Double)index / 4);
        float column = index - line * 4;
        float x = -48 + column * 44; 
        float y = 20 + line * -44; 
        Vector3 position = new Vector2(x, y);
        heartObj.GetComponent<RectTransform>().localPosition = position;
    }

    private void CleanLifeBar(){
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
    }
}
