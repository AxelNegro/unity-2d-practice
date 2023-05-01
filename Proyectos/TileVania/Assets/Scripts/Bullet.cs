using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 1f;
    PlayerMovement player;
    Rigidbody2D rigidBody;
    float xSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
        FlipSprite();
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.velocity = new Vector2(xSpeed, 0f);
    }

    void FlipSprite()
    {
        transform.localScale = new Vector2(transform.localScale.x * Mathf.Sign(xSpeed), transform.localScale.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

}
