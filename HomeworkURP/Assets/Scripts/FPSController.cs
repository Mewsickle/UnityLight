using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    
    [SerializeField] private Animator animator;

    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;


    }



    // Update is called once per frame
    void Update()
    {
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        animator.SetFloat(name: "Speed", rb.velocity.magnitude);

        MyInput();
        SpeedControl(); 

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }
    
    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //when to jump
       if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            animator.SetBool(name:"Jump", true);

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction 
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (grounded && horizontalInput != 0 || grounded && verticalInput != 0)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            animator.SetBool(name: "Run", true);
        }

        
        //if(grounded && horizontalInput > 0.01 || verticalInput > 0.1 || horizontalInput < -0.01 || verticalInput < -0.1)
        //{
        //    rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

                //        //animator.SetBool(name: "Run", true);


                //}
                //else if (horizontalInput < 0.01 || verticalInput < 0.1 || horizontalInput > -0.01 || verticalInput > -0.1)
                //{
                //    //animator.SetBool(name: "Run", false);
                //}
       else if(!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    } 

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    
    private void Jump()
    {
        Debug.Log("jumped");
        //reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);


    }

    private void ResetJump()
    {
        readyToJump = true;
        animator.SetBool(name: "Jump", false);
    }
}
