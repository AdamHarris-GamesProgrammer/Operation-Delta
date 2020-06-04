using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 12.0f;
    [SerializeField] private float jumpHeight = 3.0f;
    [SerializeField] private float gravityFactor = 2.0f;

    [Header("Movement Speed Factors")]
    [SerializeField] private float sprintSpeedFactor = 1.5f;
    [SerializeField] private float crouchSpeedFactor = 0.5f;

    [Header("Ground Settings")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] public LayerMask groundMask;


    [Header("Camera")]
    [SerializeField] private float yOffsetOnCrouch = -1.25f;
    private Transform cameraTransform;


    private float speedFactor = 1.0f;

    bool isGrounded;
    bool isCrouched = false;
    bool isSprinting = false;

    Vector3 velocity;

    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Grounded Check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        float yGravity = Physics.gravity.y * gravityFactor;

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //Crouch
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouched = !isCrouched;

            CrouchHeight();
        }

        //Sprinting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
            if (isCrouched)
            {
                isCrouched = false;
                CrouchHeight();
            }
        }
        else
        {
            isSprinting = false;
        }


        //Movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;


        if (isCrouched)
        {
            speedFactor = crouchSpeedFactor;
        }
        else if (isSprinting)
        {
            speedFactor = sprintSpeedFactor;
        }
        else
        {
            speedFactor = 1.0f;
        }

        float finalSpeed = speed * speedFactor;
        Debug.Log("Calculated Speed: " + finalSpeed);


        characterController.Move(move * finalSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * yGravity);
        }

        velocity.y += yGravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }

    void CrouchHeight()
    {
        if (isCrouched)
        {
            cameraTransform.position = new Vector3(cameraTransform.position.x, (cameraTransform.position.y + yOffsetOnCrouch), cameraTransform.position.z);
        }
        else
        {
            cameraTransform.position = new Vector3(cameraTransform.position.x, (cameraTransform.position.y - yOffsetOnCrouch), cameraTransform.position.z);
        }
    }
}
