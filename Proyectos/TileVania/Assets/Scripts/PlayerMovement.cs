using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 20f;
    [SerializeField] float climbSpeed = 1f;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    Vector2 moveInput;
    Rigidbody2D rigidBody;
    Animator animator;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D feetCollider;
    PlayerInput playerInput;
    float originalGravity;
    bool isAlive = true;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        playerInput = GetComponent<PlayerInput>();
        originalGravity = rigidBody.gravityScale;
    }

    void Update()
    {
        if (!isAlive) return;
        Run();
        FlipSprite();
        ClimbLadder();
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) return;

        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) return;

        if (!(feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))||bodyCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))) return;

        if (value.isPressed)
        {
            rigidBody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) return;

        Instantiate(bullet, gun.position, new Quaternion(0,0,0,0));

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
        if (!bodyCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Die();
    }

    void Die()
    {
        if (!isAlive) return;

        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies"))||bodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
            isAlive = false;
            animator.SetTrigger("Dying");
            rigidBody.velocity += deathKick;
        }
    }
}
