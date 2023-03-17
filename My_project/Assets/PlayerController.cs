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

   public GameObject parasiteAttack;

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
                rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
                animator.SetBool("isMoving", true);
            } else
                animator.SetBool("isMoving", false);   

            if (movementInput.x < 0)
                spriteRenderer.flipX = true;
            else if (movementInput.x > 0)
                spriteRenderer.flipX = false;
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

    void OnFire() {
        animator.SetTrigger("swordAttack");
    }

    void OnParasite() {
        // if (spriteRenderer.flipX == true)
        //     GameObject parasite = Instantiate(parasiteAttack, transform.position, Quaternion.identity);
        // else
        GameObject parasite = Instantiate(parasiteAttack, transform.position, Quaternion.identity);
        if (spriteRenderer.flipX == true)
            parasite.GetComponent<Rigidbody2D>().velocity = new Vector2(-1.0f, 0.0f);
        else
            parasite.GetComponent<Rigidbody2D>().velocity = new Vector2(1.0f, 0.0f);
        ParasiteAttack parasiteScript = parasite.GetComponent<ParasiteAttack>();
        parasiteScript.player = gameObject;
        Destroy(parasite, 2.0f);
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
