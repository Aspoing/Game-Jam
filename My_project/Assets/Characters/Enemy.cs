using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float damage = 1;

    void OnCollisionEnter2D(Collision2D other) {
        IDamageable damageable = other.collider.GetComponent<IDamageable>();

        if (damageable != null) {
            damageable.OnHit(damage);
        }
    }
}
