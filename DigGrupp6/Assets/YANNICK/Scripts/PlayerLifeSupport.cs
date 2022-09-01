using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeSupport : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float maxShield = 10f;

    private void Awake()
    {
        
    }

    void Start()
    {
        maxHealth = 100f;
        maxShield = 10f;
    }

    void Update()
    {
        if (maxHealth <= 0)
        {
            PlayerDeath();
        }
    }

    public void TakeDamage(int damageToTake)
    {
        maxShield -= damageToTake;

        if (maxShield <= 0)
        {
            maxHealth -= damageToTake;
        }

        maxShield = 0;
    }

    public void GainHealth(int healthToGain)
    {
        maxHealth += healthToGain;
    }

    public void PlayerDeath()
    {
        // Play the death animation, die, respawn at the last checkpoint
    }
}
