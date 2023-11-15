using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    [Header("KeyBinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Movement")]
    public float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;

    public float rSpeed;
    private readonly float rStop = 0.1f;
    private readonly float rMove = 7f;

    private bool attacking;

    [Header("Crouching")]
    public float crouchSpeed;
    private float crouchYScale;
    private float startYScale;
    bool crouching;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;


    public float groundDrag;

    [Header("Ground Check")]
    public LayerMask whatIsGround;
    bool grounded;
    public float extraHeight = 0.1f;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool canJump;

    [Header("Dodge Roll")]
    public float invDdelay;
    public float invDuration;

    public float DodgeCd;
    private float actCd;

    public float pushAmt;

    [Header("Step up")]
    public GameObject stepRayUpper;
    public GameObject stepRayLower;

    public float stepHeight;
    public float stepSmooth;
    [Space(30)]


    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    public Transform orientation;
    public Animator anim;
    public Transform playerObj;

    PlayerHealth hp;
    Rigidbody rb;
    //BoxCollider boxCollider;
    CapsuleCollider capsuleCollider;

    public WeaponController wp;


    public MovementState state;

    public enum MovementState
    {
        idle,
        walking,
        sprinting,
        crouching,
        air
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //boxCollider = GetComponent<BoxCollider>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        hp = GetComponent<PlayerHealth>();
        wp = GetComponentInChildren<WeaponController>();

        rb.freezeRotation = true;

        canJump = true;
        crouching = false;

        startYScale = capsuleCollider.height;
        crouchYScale = capsuleCollider.height;

        stepRayUpper.transform.position = new Vector3(stepRayUpper.transform.position.x, stepHeight, stepRayUpper.transform.position.z);

    }

    private void Update()
    {
        if(hp.isDead) return;
        attacking = anim.GetBool("isAttacking");
        MyInput();
        Animations();
        IsGrounded();

        SpeedControl();
        StateHandler();

    }

    private void FixedUpdate()
    {
        if (!attacking)
        {
            MovePLayer();
            StepClimb();
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        bool Roll = Input.GetMouseButtonDown(1);

        if (grounded)
        {

            //when presses mouse button1 attacks
            if (Input.GetButtonUp("Fire1"))
            {
                anim.SetTrigger("Attack");
                anim.SetBool("isAttacking", true);
            }

            //when jump
            else if (Input.GetKey(jumpKey) && canJump && !crouching)
            {
                canJump = false;
                Jump();
                Invoke(nameof(ResetJump), jumpCooldown);
            }

            else if (Input.GetKeyDown(crouchKey))
                crouching = !crouching;
        }

        if (actCd <= 0)
        {
            if (Roll) Dodge();
        }
        else actCd -= Time.deltaTime;
    }

    private void IsGrounded()
    {
        //ground 
        grounded = Physics.Raycast(capsuleCollider.bounds.center, Vector3.down, capsuleCollider.bounds.extents.y + extraHeight, whatIsGround);

        //handle drag
        if (grounded) rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void StateHandler()
    {

        //Mode - Crouching
        if (crouching)
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
            capsuleCollider.height = crouchYScale;
        }

        //Mode - Air
        else if (!grounded)
        {
            state = MovementState.air;

            if (moveSpeed < sprintSpeed)
                moveSpeed = walkSpeed;
            else moveSpeed = sprintSpeed;
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            moveSpeed = 0;
        }

        //Mode - Idle
        else if (moveDirection == Vector3.zero)
        {
            state = MovementState.idle;
        }

        //Mode - Sprinting
        else if (Input.GetKey(sprintKey) && grounded)
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        //Mode - Walking
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        if (!crouching) capsuleCollider.height = startYScale;
    }

    private void MovePLayer()
    {

        //calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //on slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(10f * moveSpeed * GetSlopeMoveDirection(), ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 20f, ForceMode.Force);
        }

        //on ground
        else if (grounded)
        {
            rb.AddForce(10f * moveSpeed * moveDirection.normalized, ForceMode.Force);
        }

        //in air
        else if (!grounded)
            rb.AddForce(10f * airMultiplier * moveDirection.normalized, ForceMode.Force);

        rb.useGravity = !OnSlope();
    }


    private void SpeedControl()
    {
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        else
        {
            Vector3 flatVel = new(rb.velocity.x, 0f, rb.velocity.z);

            //limit velocity if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limtedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limtedVel.x, rb.velocity.y, limtedVel.z);
            }
        }
    }

    private void Animations()
    {
        //if in air, plays jump or fall animation
        if (!grounded)
        {
            //if going down, plays falling
            if (rb.velocity.y < 0)
            {
                anim.SetBool("Falling", true);
            }
            else anim.SetBool("Jumping", true);
        }
        else if (grounded)
        {
            anim.SetBool("Falling", false);
            anim.SetBool("Jumping", false);

            anim.SetBool("Crouch", state == MovementState.crouching);

            float actualSpeed = rb.velocity.magnitude;

            //if (actualSpeed < 0)
            //    actualSpeed = 0;

            anim.SetFloat("Velocity", actualSpeed);
        }
        wp.enableAttack = attacking;

        if (wp.enableAttack) rSpeed = rStop;
        else rSpeed = rMove;


    }

    private void Jump()
    {
        exitingSlope = true;

        //reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        canJump = true;
        exitingSlope = false;
    }

    void StepClimb()
    {
        RaycastHit hitLower;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(moveDirection), out hitLower, 0.1f))
        {
            RaycastHit hitUpper;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(moveDirection), out hitUpper, 0.2f))
            {
                rb.position -= new Vector3(0f, -stepSmooth, 0f);
            }
        }
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, capsuleCollider.bounds.extents.y + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }
    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

    private void Dodge()
    {
        actCd = DodgeCd;

        hp.Invinsible(invDdelay, invDuration);

        rb.AddForce(100f * pushAmt * Time.deltaTime * playerObj.forward, ForceMode.Force);

        anim.SetTrigger("Roll");
    }
}