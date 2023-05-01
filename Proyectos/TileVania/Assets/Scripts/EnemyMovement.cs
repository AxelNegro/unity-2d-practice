using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D rigidBody;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigidBody.velocity = new Vector2(moveSpeed, rigidBody.velocity.y);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            moveSpeed = -moveSpeed;
            FlipSprite();
        }
    }

    private void FlipSprite()
    {
        transform.localScale = new Vector2(-Mathf.Sign(rigidBody.velocity.x), 1f);
    }
}
