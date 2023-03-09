using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Bounce : MonoBehaviour
{
    public float bounceForce;
    private Rigidbody rb;
    private ParticleSystem particle;
    private AudioSource aS;
    [SerializeField] Animator anim;

    void Start()
    {
        aS = GetComponent<AudioSource>();
        rb = FindObjectOfType<PlayerMove>().GetComponent<Rigidbody>();
        particle = transform.parent.GetComponentInChildren<ParticleSystem>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            aS.Play();
            particle.Play();
            anim.SetTrigger("Bounce");
            Vector3 forceInput = transform.up;
            rb.AddForce(bounceForce * forceInput);
        }
    }
}
