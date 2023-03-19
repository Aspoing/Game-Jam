using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableCharacter : MonoBehaviour, IDamageable
{
    public bool disableSumulation = false;
    Animator animator;
    Rigidbody2D rb;
    Collider2D physicsCollider;

    public HealthBar healthBar;

    bool isAlive = true;

    public float Health {
        get { return health; }
        set {
            if (value < health) {
                Hit(health);
            }

            health = value;

            if (health <= 0) {
                animator.SetBool("isAlive", false);
                Defeated();
                Targetable = false;
                if (transform.gameObject.tag == "Player")
                    GameObject.Find("GameLogic").GetComponent<GameLogic>().spawnPlayer(transform.gameObject);
            }
        }
    }

    public bool Targetable {
        set {
            targetable = value;
            if (disableSumulation)
                rb.simulated = false;
            physicsCollider.enabled = value;
        }
        get { return targetable; }
    }

    public float health = 2;
    public float maxHealth = 2;
    public bool targetable = true;

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();

        health = maxHealth;
        animator.SetBool("isAlive", isAlive);
        rb = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();
    }

    public void OnHit(float damage, Vector2 knockback) {
        Health -= damage;
        rb.AddForce(knockback, ForceMode2D.Impulse);
    }

    public void OnHit(float damage) {
        Health -= damage;
    }

    public void Hit(float health) {
        animator.SetTrigger("hit");
        if (transform.gameObject.tag == "Player")
            healthBar.setHealth(health);
    }

    public void Defeated() {
        animator.SetTrigger("Defeated");
    }

    public void OnObjectDestroyed() {
        Destroy(gameObject);
    }
}
