using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    [Header("KeyBinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;


    public float groundDrag;

    [Header("Ground Check")]
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool canJump;
    bool isJumping;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    public Animator anim;

    Rigidbody rb;


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
        rb.freezeRotation = true;

        canJump = true;

        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, 1, whatIsGround);

        MyInput();
        SpeedControl();
        StateHandler();
        Animations();

        //handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

    }

    private void FixedUpdate()
    {
        MovePLayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");


        //when jump
        if (Input.GetKey(jumpKey) && canJump && grounded)
        {
            canJump = false;
            Jump();
            isJumping = true;

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        //if (state != MovementState.air)
        //{
        //    //start crouch
        //    if (Input.GetKeyDown(crouchKey))
        //    {
        //        transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
        //        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        //    }

        //    //stop Crouch
        //    if (Input.GetKeyUp(crouchKey))
        //    {
        //        transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        //    }
        //}

    }

    private void StateHandler()
    {

        //Mode - Crouching
        if (Input.GetKey(crouchKey) && grounded)
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }

        //Mode - Air
        else if (!grounded)
        {
            state = MovementState.air;

            if (moveSpeed < sprintSpeed)
                moveSpeed = walkSpeed;
            else moveSpeed = sprintSpeed;
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
    }

    private void MovePLayer()
    {

        //calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //on ground
        if (grounded)
        {
            rb.AddForce(10f * moveSpeed * moveDirection.normalized, ForceMode.Force);
        }

        //in air
        else if (!grounded)
            rb.AddForce(10f * airMultiplier * moveDirection.normalized, ForceMode.Force);
    }


    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limtedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limtedVel.x, rb.velocity.y, limtedVel.z);
        }
    }

    private void Animations()
    {
        if (!grounded)
        {
            if (state == MovementState.air)
            {
                if (rb.velocity.y < 0)
                {
                    anim.SetBool("Falling", true);
                }
                else anim.SetBool("Jumping", true);
            }
        }
        else if (grounded)
        {
            anim.SetBool("Falling", false);
            anim.SetBool("Jumping", false);
            if (state == MovementState.idle)
            {
                anim.SetBool("Walking", false);
                anim.SetBool("Sprinting", false);
            }
            else if (state != MovementState.crouching)
            {
                anim.SetBool("Walking", true);
                anim.SetBool("Crouching", false);
            }

            //crouching
            if (state == MovementState.crouching)
            {
                anim.SetBool("Walking", false);
                anim.SetBool("Sprinting", false);
                anim.SetBool("Crouching", true);
                if (moveDirection == Vector3.zero)
                    anim.SetBool("CrouchWalk", false);
                else anim.SetBool("CrouchWalk", true);
            }
            //sprinting
            else if (state == MovementState.sprinting)
                anim.SetBool("Sprinting", true);
            else
            {
                anim.SetBool("Sprinting", false);
                anim.SetBool("Crouching", false);
                anim.SetBool("CrouchWalk", false);
            }
        }
    }

    private void Jump()
    {
        //reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        canJump = true;
    }

}