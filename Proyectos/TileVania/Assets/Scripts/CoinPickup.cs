using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickup;
    [SerializeField] int coinPoints = 100;
    bool picked = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&&!picked)
        {
            picked = true;
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(coinPickup, Camera.main.transform.position);
            FindObjectOfType<GameSession>().ProcessCoinPickup(coinPoints);
        }
    }
}
