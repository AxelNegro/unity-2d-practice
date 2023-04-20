using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceDetector : MonoBehaviour
{
    [SerializeField] ParticleSystem sliceEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        sliceEffect.Play();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        sliceEffect.Stop();
    }
}
