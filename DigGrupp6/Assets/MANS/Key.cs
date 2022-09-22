using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyId;
    public ParticleSystem particle;
    KeyManager keyManager;

    void Start()
    {
        keyManager = FindObjectOfType<KeyManager>();
        if (keyManager.ContainsKey(keyId))
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            particle.transform.parent = null;
            particle.Play();
            Destroy(particle.gameObject, 10);

            keyManager.AddKey(keyId);
            Destroy(gameObject);
        }
    }
}
