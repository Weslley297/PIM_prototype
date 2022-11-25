using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float shootWidth;
    public float attackReloadTime;
    public bool shooter;

    private float timer = 0;
    private Vector2 attackDirection;
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

        SetAttackDirection(targetPosition);
        RaycastHit2D hit = Physics2D.Raycast(
            GetShootPosition(), 
            getAttackDirection()
        );

        return hit.collider != null && hit.collider.tag.Equals("Player");
    }

    private bool CanLinearShoot(Vector3 targetPosition){
        var targetDistance = transform.position - targetPosition;
        return (targetDistance.x > 0 && CheckWith(targetDistance.y))
            || (targetDistance.x < 0 && CheckWith(targetDistance.y))
            || (targetDistance.y > 0 && CheckWith(targetDistance.x))
            || (targetDistance.y < 0 && CheckWith(targetDistance.x));
    }

    public void SetAttackDirection(Vector3 targetPosition){
        var targetDistance = transform.position - targetPosition;

        attackDirection = (targetDistance.x > 0 && CheckWith(targetDistance.y)) ? LEFT
            : (targetDistance.x < 0 && CheckWith(targetDistance.y)) ? RIGHT
            : (targetDistance.y > 0 && CheckWith(targetDistance.x)) ? DOWN
            : (targetDistance.y < 0 && CheckWith(targetDistance.x)) ? UP
            : new Vector2();
    }

    public Vector2 getAttackDirection(){
        return attackDirection;
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
        bulletMoviment.SetDirection(attackDirection);
    }

    private Vector2 GetShootPosition(){
        var size = attackDirection * (sprite.bounds.size / 2);
        return new Vector2(transform.position.x + size.x, transform.position.y + size.y);
    }

    public void ReloadAttack(){
        timer = 0;
    }
}
