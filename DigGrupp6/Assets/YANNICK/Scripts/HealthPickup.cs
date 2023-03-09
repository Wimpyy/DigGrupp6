using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthGainValue = 5;

    public YoinkerScript yoinker;

    private void Start()
    {
        yoinker = FindObjectOfType<YoinkerScript>();

    }

    
}
