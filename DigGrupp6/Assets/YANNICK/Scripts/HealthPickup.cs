using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthGainValue = 5;

    private void OnCollisionEnter(Collision collision)
    {
        FindObjectOfType<PlayerLifeSupport>().GainHealth(healthGainValue);
    }
}
