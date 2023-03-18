using UnityEngine;

public class Projectile : MonoBehaviour{
    public float speed = 70;
    public float lifeTime = .5f;

    public GameObject explosion;

    public int damage = 1;
    public GameObject soundObject;

    private void Start(){
        Invoke(nameof(DestroyProjectile), lifeTime);
        Instantiate(soundObject, transform.position, transform.rotation);
    }

    private void Update(){
        transform.Translate(Vector2.up * (speed * Time.deltaTime));
    }

    void DestroyProjectile(){
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col){
        if (col.CompareTag("Enemy")){
            col.GetComponent<Enemy>().TakeDamage(damage);
            DestroyProjectile();
        }
        else if (col.CompareTag("boss")){
            col.GetComponent<Boss>().TakeDamage(damage);
            DestroyProjectile();
        }
    }
}