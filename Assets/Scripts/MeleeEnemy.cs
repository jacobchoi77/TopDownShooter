using System.Collections;
using UnityEngine;

public class MeleeEnemy : Enemy{
    public float stopDistance;

    private float attackTime;

    public float attackSpeed;

    private void Update(){
        if (player != null){
            if (Vector2.Distance(transform.position, player.position) > stopDistance){
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else{
                if (Time.time >= attackTime){
                    StartCoroutine(Attack());
                    attackTime = Time.time + timeBetweenAttack;
                }
            }
        }
    }

    IEnumerator Attack(){
        player.GetComponent<Player>().TakeDamage(damage);
        var originalPosition = transform.position;
        var targetPosition = player.position;

        float percent = 0;
        while (percent <= 1){
            percent += Time.deltaTime * attackSpeed;
            var formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }
    }
}