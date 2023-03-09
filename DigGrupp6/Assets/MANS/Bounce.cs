using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Bounce : MonoBehaviour
{
    public float bounceForce;
    private Rigidbody rb;
    private ParticleSystem particle;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = FindObjectOfType<PlayerMove>().GetComponent<Rigidbody>();
        particle = transform.parent.GetComponentInChildren<ParticleSystem>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            particle.Play();
            audioSource.Play();
            rb.AddForce(0, bounceForce, 0);
        }
    }
}
