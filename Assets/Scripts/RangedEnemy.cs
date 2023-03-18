using UnityEngine;

public class RangedEnemy : Enemy{
    public float stopDistance;
    private float attackTime;
    private Animator anim;
    public Transform shotPoint;
    public GameObject enemyBullet;

    public override void Start(){
        base.Start();
        anim = GetComponent<Animator>();
    }

    private void Update(){
        if (player != null){
            if (Vector2.Distance(transform.position, player.position) > stopDistance){
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }

            if (Time.time >= attackTime){
                attackTime = Time.time + timeBetweenAttack;
                anim.SetTrigger("attack");
            }
        }
    }

    public void RangedAttack(){
        if (player == null) return;
        var direction = player.position - shotPoint.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        shotPoint.rotation = rotation;
        Instantiate(enemyBullet, shotPoint.position, shotPoint.rotation);
    }
}