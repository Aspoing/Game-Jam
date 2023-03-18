using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetroidScript : MonoBehaviour
{

    public float damage = 1;
    public float knockbackForce = 10f;
    public float moveSpeed = 10f;
    private float maxHealth = 100;
    private bool isBelowMid = false;

    public float cooldownTime = 6;
    private float nextParasiteTime = 0;
    public GameObject parasiteAttack;

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
        if (damageableCharacter.Health < maxHealth / 2 && !isBelowMid) {
            animator.SetBool("isBelowMid", true);
            moveSpeed = 12000f;
            isBelowMid = true;
            transform.gameObject.tag = "EnemyImmuneSword";
        }
        if (damageableCharacter.Targetable && detectionZone.detectObjs.Count > 0) {
            Vector2 direction = (detectionZone.detectObjs[0].transform.position - transform.position).normalized;
            rb.AddForce(direction * moveSpeed * Time.deltaTime);
            Debug.Log(direction * moveSpeed * Time.deltaTime);
            // rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);

            if (Time.time > nextParasiteTime) {
                nextParasiteTime = Time.time + cooldownTime;
                GameObject parasite = Instantiate(parasiteAttack, transform.position, Quaternion.identity);
                parasite.GetComponent<Rigidbody2D>().velocity = new Vector2(
                    (detectionZone.detectObjs[0].transform.position.x - transform.position.x) * (isBelowMid == false ? 2 : 5),
                    (detectionZone.detectObjs[0].transform.position.y - transform.position.y) * (isBelowMid == false ? 2 : 5));
                ParasiteAttack parasiteScript = parasite.GetComponent<ParasiteAttack>();
                parasiteScript.player = gameObject;
                parasiteScript.target = "Player";
                parasiteScript.damage = 2;
                Destroy(parasite, 2.0f);
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
