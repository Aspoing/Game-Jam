using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // public float moveSpeed = 150f;
    // public float maxSpeed = 8f;
    // public float idleFriction = 0.9f;
   public float moveSpeed = 1f;
   public float collisionOffset = 0.05f;
   public ContactFilter2D movementFilter;
   public SwordAttack swordAttack;

    Vector2 movementInput = Vector2.zero;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    // List<RaycastHit2D> castCollisison = new List<RaycastHit2D>();

    bool canMove = true;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
       if (canMove) {
            if (movementInput != Vector2.zero) {
                bool success = TryMove(movementInput);

                if (!success && movementInput.x > 0)
                    success = TryMove(new Vector2(movementInput.x, 0));
                if (!success && movementInput.y > 0)
                    success = TryMove(new Vector2(0, movementInput.y));

                animator.SetBool("isMoving", success);
            } else {
                animator.SetBool("isMoving", false);   
                // rb.velocity = Vector2.ClampMagnitude(rb.velocity + (movementInput * moveSpeed * Time.deltaTime), maxSpeed)
            }

            if (movementInput.x < 0) {
                spriteRenderer.flipX = true;
            } else if (movementInput.x > 0) {
                spriteRenderer.flipX = false;
            }
        }
    }

    private bool TryMove(Vector2 direction) {
        // if (direction == Vector2.zero)
        //     return false;
        // int count = rb.Cast(
        //     direction,
        //     movementFilter,
        //     castCollisison,
        //     moveSpeed * Time.fixedDeltaTime + collisionOffset
        // );
        int count = 0;
        if (count == 0) {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        // return false;
        return true;
    }

    void OnMove(InputValue value) {
        movementInput = value.Get<Vector2>();
    }

    // void UpdateAnimatorParame

    void OnFire() {
        animator.SetTrigger("swordAttack");
    }

    public void SwordAttack() {
        if (spriteRenderer.flipX == true)
            swordAttack.AttackLeft();
        else
            swordAttack.AttackRight();
    }

    public void EndSwordAttack() {
        swordAttack.StopAttack();
    }
}
