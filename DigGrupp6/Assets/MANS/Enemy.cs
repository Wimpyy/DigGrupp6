using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float movementSpeed;
    public float sightDistance;
    public float accelerationTime;
    public float deAccelerationTime;
    public float attackDuration;
    public float attackCooldown;
    public float attackDistance;
    public Collider damageCollider;

    [NonSerialized] public Rigidbody rb;
    [NonSerialized] public Transform player;

    void Start()
    {
        damageCollider.enabled = false;
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerMove>().transform;
    }

    void Update()
    {
        
    }
}
