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
        if (movementInput.x == 0 && movementInput.y == 0)
            parasite.GetComponent<Rigidbody2D>().velocity = new Vector2(spriteRenderer.flipX == true ? -2.0f : 2.0f, 0.0f);
        else
            parasite.GetComponent<Rigidbody2D>().velocity = new Vector2(
                movementInput.x > 0 ? 2.0f : (movementInput.x < 0 ? -2.0f : 0.05f),
                movementInput.y > 0 ? 2.0f : (movementInput.y < 0 ? -2.0f : 0.05f));
        ParasiteAttack parasiteScript = parasite.GetComponent<ParasiteAttack>();
        parasiteScript.player = gameObject;
        Destroy(parasite, 2.0f);
    }

    void OnResize() {

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
