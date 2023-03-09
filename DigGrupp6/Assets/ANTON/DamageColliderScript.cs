using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageColliderScript : MonoBehaviour
{
    public bool dealtDmg = false;

    Collider col;
    private void Start()
    {
        col = GetComponent<Collider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !dealtDmg)
        {
            Debug.Log("dealt");
            FindObjectOfType<PlayerLifeSupport>().TakeDamage(1);
            dealtDmg = true;
        }
    }

    private void Update()
    {
        if (col.enabled == false)
        {
            dealtDmg = false;
        }
    }
}
