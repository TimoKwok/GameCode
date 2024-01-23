using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
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

    Rigidbody rb;

    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource runningSoundEffect;


    private GameObject bubBody;
    private Animator bubAnimator;


    private bool isSitting = false;
    private bool isMoving;

    private void Start()
    {
        bubBody = transform.Find("bub").gameObject; //set "bub" to bubBody
        bubAnimator = bubBody.GetComponent<Animator>(); //grab the animator from "bub"

        readyToJump = true;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        MyInput();
        SpeedControl();

        if(grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

        //NEW CODE
        if (isMoving && grounded)
        {
            bubAnimator.SetBool("isRunning", true);
            if (!runningSoundEffect.isPlaying)
            {
                runningSoundEffect.Play();  
            }
        }
        else
        {
            runningSoundEffect.Stop();
            bubAnimator.SetBool("isRunning", false);
        }
        //NEW CODE ENDED
        if (Input.GetKey("1"))
        {
            bubAnimator.SetBool("isDancing", true);
        }
        else
        {
            bubAnimator.SetBool("isDancing", false);
        }

        if (Input.GetKeyDown("2"))
        {
            isSitting = !isSitting;
            bubAnimator.SetBool("isSitting", isSitting);
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //NEW CODE
        isMoving = Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f;
        //NEW CODE

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();
            jumpSoundEffect.Play();

            
            Invoke(nameof(ResetJump), jumpCooldown); //continye to jump if you hold
        }
    }

    private void MovePlayer()
    {
        //calculate movment direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //NEW CODE
        isMoving = Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f;
        //NEW CODE

        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        bubAnimator.SetBool("isJumping", true);
    }

    private void ResetJump()
    {
        readyToJump = true;
        bubAnimator.SetBool("isJumping", false);
    }
}
