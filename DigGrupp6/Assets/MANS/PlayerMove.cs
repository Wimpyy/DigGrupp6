using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float walkSpeed;
    public float crouchSpeed;
    public float jumpForce;
    public float earlyJumpDuration;
    public float accelerationTime;
    public float deAccelerationTime;
    public float slideDeAccelerationTime;
    public float slideDuration;
    public LayerMask groundLayer;

    private InputManager inputManager;
    public Collider coll;
    private Rigidbody rb;
    private float earlyJumpTimer;
    private bool hasJumped = false;
    private bool isSliding;
    private float slidedTime;
    private float slideDirection;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();

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
        bool isInputingMovement = Mathf.Abs(inputManager.MovementValue.x) >= 0.1f;

        if ((inputManager.IsCrouching || (isSliding && slidedTime <= slideDuration)) && IsOnGround())
        {
            transform.localScale = new Vector3(1, 0.5f, 1);

            //Start slideing.
            if (!isSliding && Mathf.Abs(rb.velocity.x) >= crouchSpeed) 
            {
                slideDirection = Mathf.Sign(inputManager.MovementValue.x);
                isSliding = true;
                slidedTime = 0;
            }

            if (isSliding && slidedTime <= slideDuration) //Slide
            {
                slidedTime += Time.deltaTime;
                ChangeVelocity(walkSpeed * slideDirection, accelerationTime);
            }
            else //Crouch
            {
                if (isInputingMovement)
                {
                    ChangeVelocity(crouchSpeed * inputManager.MovementValue.x, accelerationTime);
                }
                else
                {
                    ChangeVelocity(0, deAccelerationTime);
                }
            }
        }
        else//Walk
        {
            transform.localScale = new Vector3(1, 1, 1);

            slidedTime = 0;
            isSliding = false;

            if (isInputingMovement)
            {
                ChangeVelocity(walkSpeed * inputManager.MovementValue.x, accelerationTime);
            }
            else
            {
                ChangeVelocity(0, deAccelerationTime);
            }
        }
    }

    void ChangeVelocity(float newVelocity, float acc)
    {
        Vector3 velocity = rb.velocity;
        velocity.z = 0;
        velocity.x = Mathf.Lerp(velocity.x, newVelocity, acc * Time.deltaTime);
        rb.velocity = velocity;
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
