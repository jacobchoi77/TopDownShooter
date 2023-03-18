using UnityEngine;

public class Enemy : MonoBehaviour{
    public int health;

    [HideInInspector]
    public Transform player;

    public float speed;

    public float timeBetweenAttack;

    public int damage;

    public int pickupChance;
    public int healthPickupChance;
    public GameObject[] pickups;
    public GameObject healthPickup;

    public GameObject deathEffect;
    public GameObject deathSound;

    public virtual void Start(){
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void TakeDamage(int damageAmount){
        health -= damageAmount;
        if (health <= 0){
            Instantiate(deathSound, transform.position, transform.rotation);
            int randomNumber = Random.Range(0, 101);
            if (randomNumber < pickupChance){
                var randomPickup = pickups[Random.Range(0, pickups.Length)];
                Instantiate(randomPickup, transform.position, transform.rotation);
            }

            int randHealth = Random.Range(0, 101);
            if (randHealth < healthPickupChance){
                Instantiate(healthPickup, transform.position, transform.rotation);
            }

            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}