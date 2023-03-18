using UnityEngine;

public class Pickup : MonoBehaviour{
    public Weapon weaponToEquip;

    public GameObject effect;

    private void OnTriggerEnter2D(Collider2D col){
        if (col.CompareTag("Player")){
            Instantiate(effect, transform.position, Quaternion.identity);
            col.GetComponent<Player>().ChangeWeapon(weaponToEquip);
            Destroy(gameObject);
        }
    }
}