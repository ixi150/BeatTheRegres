using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] ParticleSystem dieEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            if (dieEffect)
            {
                Instantiate(dieEffect, transform.position, dieEffect.transform.rotation);
            }
        }
    }
}