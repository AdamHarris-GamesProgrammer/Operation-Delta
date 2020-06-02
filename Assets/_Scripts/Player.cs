using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 2.5f;
    [SerializeField] private float jumpHeight = 1.5f;

    [Header("Rotation Settings")]
    [SerializeField] private float lookSpeed = 2.5f;

    [SerializeField] GameObject camera;

    private Rigidbody playerRb;

    float jumpPower = 0.0f;
    bool isGrounded = false;
    bool isJumping = false;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
        }

        if (isJumping && isGrounded)
        {
            jumpPower = jumpHeight * Time.deltaTime;
        }

        if (!isGrounded && isJumping)
        {
            jumpPower -= Time.deltaTime;
        }

        if(jumpPower <= 0.0f)
        {
            jumpPower = 0.0f;
            isJumping = false;
        }

        transform.Translate(new Vector3(horizontalInput * movementSpeed * Time.deltaTime, jumpPower, verticalInput * movementSpeed * Time.deltaTime));


        float horizontalLook = lookSpeed * Input.GetAxis("Mouse X");

        transform.Rotate(new Vector3(0.0f, horizontalLook));

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
