using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public string keyId;
    KeyManager keyManager;
    
    void Start()
    {
        keyManager = FindObjectOfType<KeyManager>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (keyManager.ContainsKey(keyId))
            {
                Destroy(gameObject);
            }
        }
    }
}
