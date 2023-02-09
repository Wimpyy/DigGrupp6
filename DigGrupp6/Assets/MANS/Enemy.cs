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
    public float attackWindupDuration;
    public float attackDamageDuration;
    public float attackCooldown;
    public float attackDistance;
    public Collider damageCollider;

    [NonSerialized] public Animator anim;
    [NonSerialized] public Rigidbody rb;
    [NonSerialized] public PlayerMove player;

    void Start()
    {
        damageCollider.enabled = false;
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerMove>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        
    }
}
