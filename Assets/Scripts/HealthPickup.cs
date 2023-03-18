using UnityEngine;

public class HealthPickup : MonoBehaviour{
    private Player playerScript;
    public int healAmount;
    public GameObject effect;

    private void Start(){
        playerScript = FindObjectOfType<Player>().GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D col){
        if (col.CompareTag("Player")){
            Instantiate(effect, transform.position, Quaternion.identity);
            playerScript.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}