using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCowScript : MonoBehaviour
{

    public float damage = 1;
    public float knockbackForce = 10f;

    public float cooldownTime = 4;
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
        if (damageableCharacter.Targetable && detectionZone.detectObjs.Count > 0) {
            if (Time.time > nextParasiteTime) {
                nextParasiteTime = Time.time + cooldownTime;
                GameObject parasite = Instantiate(parasiteAttack, transform.position, Quaternion.identity);
                parasite.GetComponent<Rigidbody2D>().velocity = new Vector2(
                    (detectionZone.detectObjs[0].transform.position.x - transform.position.x) * 2,
                    (detectionZone.detectObjs[0].transform.position.y - transform.position.y) * 2);
                ParasiteAttack parasiteScript = parasite.GetComponent<ParasiteAttack>();
                parasiteScript.player = gameObject;
                parasiteScript.target = "Player";
                parasiteScript.damage = 1;
                Destroy(parasite, 2.0f);
            }
        }
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
