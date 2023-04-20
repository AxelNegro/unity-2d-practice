using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    [SerializeField] ParticleSystem crashEffect;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            crashEffect.Play();
            Invoke("ReloadScene", delay);
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
