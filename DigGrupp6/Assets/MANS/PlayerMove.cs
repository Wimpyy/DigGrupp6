using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float walkSpeed;
    public float jumpForce;
    public float earlyJumpDuration;
    public float accelerationTime;
    public float deAccelerationTime;
    public LayerMask groundLayer;

    private InputManager inputManager;
    private Collider coll;
    private Rigidbody rb;
    private float earlyJumpTimer;
    private bool hasJumped = false;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();

        inputManager.JumpEvent += TryJump;
    }

    void Update()
    {
        earlyJumpTimer -= Time.deltaTime;

        if (IsOnGround() && earlyJumpTimer > 0)
        {
            earlyJumpTimer = 0;
            DoJump();
        }

        Move();
    }

    private void LateUpdate()
    {
        hasJumped = false;
    }

    void Move()
    {
        Vector3 vel = rb.velocity;
        vel.z = 0;

        if (inputManager.MovementValue.x != 0)
        {
            //vel.x = inputManager.MovementValue.x * walkSpeed;

            vel.x = Mathf.Lerp(vel.x, walkSpeed * inputManager.MovementValue.x, accelerationTime * Time.deltaTime);
        }
        else
        {
            vel.x -= vel.x * (deAccelerationTime + 1) * Time.deltaTime;
        }

        rb.velocity = vel;
    }

    void TryJump()
    {
        if (IsOnGround())
        {
            DoJump();
        }
        else
        {
            earlyJumpTimer = earlyJumpDuration;
        }
    }

    void DoJump()
    {
        if (hasJumped) { return; }

        hasJumped = true;
        Vector3 vel = rb.velocity;
        vel.y = 0;
        rb.velocity = vel;
        rb.AddForce(new Vector3(0, jumpForce, 0));
    }

    bool IsOnGround()
    {
        Vector3 extents = coll.bounds.size;
        Vector3 center = coll.bounds.center;
        extents.x *= 0.9f;
        extents.z *= 0.9f;
        extents /= 2;
        center.y -= 0.01f;

        return Physics.CheckBox(center, extents, Quaternion.identity, groundLayer);
    }
}
