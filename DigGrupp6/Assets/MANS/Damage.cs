using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] int damage;

    PlayerLifeSupport playerLifeSupport;

    void Start()
    {
        playerLifeSupport = FindObjectOfType<PlayerLifeSupport>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerLifeSupport.TakeDamage(damage);
        }
    }
}
