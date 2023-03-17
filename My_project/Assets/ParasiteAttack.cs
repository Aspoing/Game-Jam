using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteAttack : MonoBehaviour
{
    public float damage = 5;
    public float knockbackForce = 30f;

    public Vector2 velocity = new Vector2(0.0f, 0.0f);
    public GameObject player;
    public string target = "Enemy";

    void FixedUpdate() {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPosition = currentPosition + velocity * Time.deltaTime;

        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition, newPosition);

        foreach (RaycastHit2D hit in hits) {
            GameObject other = hit.collider.gameObject;

            if (other != player) {
                IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();
                Vector2 direction = (Vector2) (hit.collider.transform.position - transform.position).normalized;
                Vector2 knockback = direction * knockbackForce;

                if (other.gameObject.tag == target || other.gameObject.tag == "DecorCollision") {
                    damageableObject.OnHit(damage);
                    Destroy(gameObject);
                }
            }
        }
        transform.position = newPosition;
    }
}
