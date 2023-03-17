using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableCharacter : MonoBehaviour, IDamageable
{
    public bool disableSumulation = false;
    Animator animator;
    Rigidbody2D rb;
    Collider2D physicsCollider;

    bool isAlive = true;

    public float Health {
        get { return health; }
        set {
            if (value < health) {
                Hit();
            }

            Debug.Log("Est ce que tu entres ici ?");
            health = value;

            if (health <= 0) {
                animator.SetBool("isAlive", false);
                Defeated();
                Targetable = false;
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
    public bool targetable = true;

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();

        animator.SetBool("isAlive", isAlive);
        rb = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();
    }

    // void OnHit(float damage) {
    //     Health -= damage;
    // }

    public void OnHit(float damage, Vector2 knockback) {
        Health -= damage;
        rb.AddForce(knockback, ForceMode2D.Impulse);
        Debug.Log("Force : " + knockback);
    }

    public void OnHit(float damage) {
        Health -= damage;
    }

    public void Hit() {
        animator.SetTrigger("hit");
    }

    public void Defeated() {
        animator.SetTrigger("Defeated");
    }

    public void OnObjectDestroyed() {
        Destroy(gameObject);
    }
}
