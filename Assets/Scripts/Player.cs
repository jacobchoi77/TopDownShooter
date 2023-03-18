using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour{
    public float speed = 30;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveAmount;

    public int health;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Animator hurtAnim;

    public GameObject hurtSound;
    private SceneTransitions sceneTransitions;

    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sceneTransitions = FindObjectOfType<SceneTransitions>();
    }

    private void Update(){
        var moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveAmount = moveInput.normalized * speed;
        anim.SetBool("isRunning", moveInput != Vector2.zero);
    }

    private void FixedUpdate(){
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }

    public void TakeDamage(int damageAmount){
        Instantiate(hurtSound, transform.position, Quaternion.identity);
        health -= damageAmount;
        UpdateHealthUI(health);
        hurtAnim.SetTrigger("hurt");
        if (health <= 0){
            Destroy(gameObject);
            sceneTransitions.LoadScene("Lose");
        }
    }

    public void ChangeWeapon(Weapon weaponToEquip){
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        Instantiate(weaponToEquip, transform.position, transform.rotation, transform);
    }

    private void UpdateHealthUI(int currentHealth){
        for (int i = 0; i < hearts.Length; i++){
            if (i < currentHealth){
                hearts[i].sprite = fullHeart;
            }
            else{
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    public void Heal(int healAmount){
        if (health + healAmount > 5){
            health = 5;
        }
        else{
            health += healAmount;
        }

        UpdateHealthUI(health);
    }
}