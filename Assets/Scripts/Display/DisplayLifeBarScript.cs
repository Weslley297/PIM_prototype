using UnityEngine;

public class DisplayLifeBarScript : MonoBehaviour
{
    public GameObject fullHeartPrefab;
    public GameObject halfHeartPrefab;
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
        var fullHearts = life / 20;
        for (int i = 1; i <= fullHearts; i++)
        {
            CreateHeart(fullHeartPrefab, index);
            index++;
        }

        var halfHearts = (life - index * 20) / 10;
        for (int i = 1; i <= halfHearts; i++)
        {
            CreateHeart(halfHeartPrefab, index);
            index++;
        }

        var emptyHearts = (maxLife - index * 20) / 20;
        for (int i = 1; i <= emptyHearts; i++)
        {
            CreateHeart(emptyHeartPrefab, index);
            index++;
        }
    }

    private void CreateHeart(GameObject heart, int index){
        var fullHeart = Instantiate(heart);
        fullHeart.transform.SetParent(gameObject.transform);
        fullHeart.transform.localScale = new Vector3(1, 1, 1);

        var position = new Vector3();
        position.x += -150 + index * 27; 
        fullHeart.GetComponent<RectTransform>().localPosition = position;
    }

    private void CleanLifeBar(){
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
    }
}
