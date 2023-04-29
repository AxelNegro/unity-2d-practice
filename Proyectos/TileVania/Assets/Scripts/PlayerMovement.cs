using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 20f;
    [SerializeField] float climbSpeed = 1f;

    Vector2 moveInput;
    Rigidbody2D rigidBody;
    Animator animator;
    CapsuleCollider2D collider;
    float originalGravity;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider2D>();
        originalGravity = rigidBody.gravityScale;
    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!(collider.IsTouchingLayers(LayerMask.GetMask("Ground"))||collider.IsTouchingLayers(LayerMask.GetMask("Ladder")))) return;

        if (value.isPressed)
        {
            rigidBody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rigidBody.velocity.y);
        rigidBody.velocity = playerVelocity;

        animator.SetBool("isRunning", Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon;

        if(playerHasHorizontalSpeed)
            transform.localScale = new Vector2(Mathf.Sign(rigidBody.velocity.x), 1f); 
    }

    void ClimbLadder()
    {
        if (!collider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            animator.SetBool("isClimbing", false);
            rigidBody.gravityScale = originalGravity;
            return;
        }

        bool playerHasVerticalSpeed = Mathf.Abs(rigidBody.velocity.y) > Mathf.Epsilon;
        animator.SetBool("isClimbing", playerHasVerticalSpeed);

        Vector2 climbVelocity = new Vector2(rigidBody.velocity.x, moveInput.y * climbSpeed);

        rigidBody.velocity = climbVelocity;
        rigidBody.gravityScale = 0;


    }
}
