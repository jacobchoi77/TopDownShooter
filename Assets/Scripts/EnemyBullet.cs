using UnityEngine;

public class EnemyBullet : MonoBehaviour{
    private Player playerScript;

    private Vector2 targetPosition;

    public float speed;

    public int damage;

    public GameObject damageEffect;

    private void Start(){
        playerScript = FindObjectOfType<Player>().GetComponent<Player>();
        targetPosition = playerScript.transform.position;
    }

    private void Update(){
        if (Vector2.Distance(transform.position, targetPosition) > .1f){
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else{
            Instantiate(damageEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col){
        if (col.CompareTag("Player")){
            playerScript.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}