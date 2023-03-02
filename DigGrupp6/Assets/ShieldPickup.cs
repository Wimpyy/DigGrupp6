using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    [SerializeField] float distanceForActivation;
    [SerializeField] Transform player;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance < distanceForActivation)
        {
            anim.SetBool("Open", true);
        }
        else
        {
            anim.SetBool("Open", false);
        }
    }
}
