using System.Collections;
using UnityEngine;

public class Summoner : Enemy{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private Vector2 targetPosition;
    private Animator anim;
    public float timeBetweenSummons;
    private float summonTime;

    public Enemy enemyToSummon;

    public float meleeAttackSpeed;
    public float stopDistance;
    private float timer;

    public override void Start(){
        base.Start();
        var randomX = Random.Range(minX, maxX);
        var randomY = Random.Range(minY, maxY);
        targetPosition = new Vector2(randomX, randomY);
        anim = GetComponent<Animator>();
    }

    private void Update(){
        if (player != null){
            if (Vector2.Distance(transform.position, targetPosition) > .5f){
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                anim.SetBool("isRunning", true);
            }
            else{
                anim.SetBool("isRunning", false);
                if (Time.time >= summonTime){
                    summonTime = Time.time + timeBetweenSummons;
                    anim.SetTrigger("summon");
                }
            }

            if (Vector2.Distance(transform.position, player.position) < stopDistance){
                if (Time.time > timer){
                    timer = Time.time + timeBetweenAttack;
                    StartCoroutine(MeleeAttack());
                }
            }
        }
    }

    private IEnumerator MeleeAttack(){
        player.GetComponent<Player>().TakeDamage(damage);
        var originalPosition = transform.position;
        var targetPosition = player.position;

        float percent = 0;
        while (percent <= 1){
            percent += Time.deltaTime * meleeAttackSpeed;
            var formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }
    }

    public void Summon(){
        if (player != null){
            Instantiate(enemyToSummon, transform.position, transform.rotation);
        }
    }
}