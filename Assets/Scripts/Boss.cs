using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour{
    public int health;
    public Enemy[] enemies;
    public float spawnOffset;
    private int halfHealth;
    private Animator anim;
    public int damage;
    public GameObject blood;
    public GameObject effect;
    private Slider healthBar;
    private SceneTransitions sceneTransitions;

    private void Start(){
        halfHealth = health / 2;
        anim = GetComponent<Animator>();
        healthBar = FindObjectOfType<Slider>();
        healthBar.maxValue = health;
        healthBar.value = health;
        sceneTransitions = FindObjectOfType<SceneTransitions>();
    }

    public void TakeDamage(int damageAmount){
        health -= damageAmount;
        healthBar.value = health;
        if (health <= 0){
            Instantiate(effect, transform.position, Quaternion.identity);
            Instantiate(blood, transform.position, Quaternion.identity);
            Destroy(gameObject);
            healthBar.gameObject.SetActive(false);
            sceneTransitions.LoadScene("Win");
        }

        if (health <= halfHealth){
            anim.SetTrigger("stage2");
        }

        var randomEnemy = enemies[Random.Range(0, enemies.Length)];
        Instantiate(randomEnemy, transform.position + new Vector3(spawnOffset, spawnOffset, 0), transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D col){
        if (col.CompareTag("Player")){
            col.GetComponent<Player>().TakeDamage(damage);
        }
    }
}