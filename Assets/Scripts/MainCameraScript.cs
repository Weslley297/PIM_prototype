using UnityEngine;

public class MainCameraScript : MonoBehaviour
{
    public float topCameraLimit, bottomCameraLimit, rightCameraLimit, leftCameraLimit;
    
    private GameObject target;
    
     void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.position = getPosition();
    }

    private Vector3 getPosition(){
        var position = new Vector3(
            target.transform.position.x, 
            target.transform.position.y, 
            transform.position.z
        );

        if(position.y < bottomCameraLimit){
            position.y = bottomCameraLimit;
        }

        if(position.y > topCameraLimit){
            position.y = topCameraLimit;
        }

        if(position.x < leftCameraLimit){
            position.x = leftCameraLimit;
        }

        if(position.x > rightCameraLimit){
            position.x = rightCameraLimit;
        }

        return position;
    }

    public void setCameraLimits(float top, float bottom, float left, float right){
        topCameraLimit = top;
        bottomCameraLimit = bottom;
        leftCameraLimit = left;
        rightCameraLimit = right;
    }
}
