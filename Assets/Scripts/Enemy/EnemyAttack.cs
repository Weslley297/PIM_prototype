using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float shootWidth;
    public float attackReloadTime;
    public bool shooter;

    private float timer = 0;
    private Vector2 linearShootDirection;
    private SpriteRenderer sprite;

    private readonly Vector2 LEFT = new Vector2(-1, 0);
    private readonly Vector2 RIGHT = new Vector2(1, 0);
    private readonly Vector2 UP = new Vector2(0, 1);
    private readonly Vector2 DOWN = new Vector2(0, -1);

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        enabled = false;
    }

    public bool CanAttack(Vector3 targetPosition){
        if(timer < attackReloadTime){
            timer += Time.deltaTime;
            return false;
        }

        if(!shooter || !CanLinearShoot(targetPosition)){
            return false;
        }

        
        var direction = SetLinearShootDirection(targetPosition);
        var position = GetShootPosition();

        RaycastHit2D hit = Physics2D.Raycast(position, direction);
        if(hit.collider == null || !hit.collider.tag.Equals("Player")){
            return false;
        } 
     
        return true;
    }

    private bool CanLinearShoot(Vector3 targetPosition){
        var targetDistance = transform.position - targetPosition;
        return (targetDistance.x > 0 && CheckWith(targetDistance.y))
            || (targetDistance.x < 0 && CheckWith(targetDistance.y))
            || (targetDistance.y > 0 && CheckWith(targetDistance.x))
            || (targetDistance.y < 0 && CheckWith(targetDistance.x));
    }

    public Vector2 SetLinearShootDirection(Vector3 targetPosition){
        var targetDistance = transform.position - targetPosition;

        linearShootDirection = (targetDistance.x > 0 && CheckWith(targetDistance.y)) ? LEFT
            : (targetDistance.x < 0 && CheckWith(targetDistance.y)) ? RIGHT
            : (targetDistance.y > 0 && CheckWith(targetDistance.x)) ? DOWN
            : (targetDistance.y < 0 && CheckWith(targetDistance.x)) ? UP
            : new Vector2();

        return linearShootDirection;
    }

    private bool CheckWith(float side){
        return side < shootWidth && side > -shootWidth;
    }

    public void Attack(){
        timer = 0;

        if(shooter){
            LinearShoot();
        }
    }

    private void LinearShoot(){
        var shootPosition = GetShootPosition();
        var bulletPosition = new Vector3(shootPosition.x, shootPosition.y, 0);

        var bullet = Instantiate(bulletPrefab, bulletPosition, Quaternion.identity);
        var bulletMoviment = bullet.GetComponent<BulletScript>();
        bulletMoviment.SetDirection(linearShootDirection);
    }

    private Vector2 GetShootPosition(){
        var size = linearShootDirection * sprite.bounds.size;
        return new Vector2(transform.position.x + size.x, transform.position.y + size.y);
    }

    public void ReloadAttack(){
        timer = 0;
    }
}
