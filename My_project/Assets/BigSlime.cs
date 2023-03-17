using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSlime : MonoBehaviour
{
    Animator animator;

    public float Health {
        set {
            if (value < health) {
                animator.SetTrigger("hit");
            }
            health = value;
            if (health <= 0) {
                animator.SetTrigger("Defeated");
            }
        }
        get {
            return health;
        }
    }

    public float health = 10;

    void Start() {
        animator = GetComponent<Animator>();
    }

    void OnHit(float damage) {
        Health -= damage;
    }
}
