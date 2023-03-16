using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float Health {
        get { return health; }
        set {
            if (value < health) {
            }

            health = value;

            if (health <= 0) {
                Targetable = false;
            }
        }
    }

    public bool Targetable {
        set {
            targetable = value;
            // rb.simulated = value;
            // physicsCollider.enabled = value;
        }
        get { return targetable; }
    }

    public float health = 3f;
    public bool targetable = true;

    public void OnHit(float damage, Vector2 knockback) {

    }

    public void OnHit(float damage) {

    }

    public void OnObjectDestroyed() {

    }

}
