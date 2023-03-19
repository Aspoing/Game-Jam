using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{

    public float damage = 1;
    public float knockbackForce = 10f;
    public float moveSpeed = 10000f;

    public float cooldownTime = 6;
    public float runningTime = 4;
    private float nextRunnningTime = 0;

    public DetectionZone detectionZone;
    Rigidbody2D rb;
    DamageableCharacter damageableCharacter;
    Animator animator;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageableCharacter = GetComponent<DamageableCharacter>();
    }

    void FixedUpdate() {
        if (damageableCharacter.Targetable && detectionZone.detectObjs.Count > 0) {
            Vector2 direction = (detectionZone.detectObjs[0].transform.position - transform.position).normalized;
            rb.AddForce(direction * moveSpeed * Time.deltaTime);

            if (Time.time > nextRunnningTime && animator.GetBool("isRunning") == false) {
                nextRunnningTime = Time.time + runningTime;
                moveSpeed = 40000f;
                animator.SetBool("isRunning", true);
            } else if (Time.time > nextRunnningTime && animator.GetBool("isRunning") == true) {
                nextRunnningTime = Time.time + cooldownTime;
                moveSpeed = 15000f;
                animator.SetBool("isRunning", false);
            }
            animator.SetBool("isMoving", true);
        } else 
            animator.SetBool("isMoving", false);
    }

    void OnCollisionEnter2D(Collision2D other) {
        Collider2D collider = other.collider;
        IDamageable damageable = collider.GetComponent<IDamageable>();

        if (damageable != null) {
            Vector2 direction = (Vector2) (collider.transform.position - transform.position).normalized;
            Vector2 knockback = direction * knockbackForce;

            damageable.OnHit(damage, knockback);
        }
    }
}
