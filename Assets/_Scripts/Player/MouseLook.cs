using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Mouse Settings")]
    [SerializeField] private float mouseSensitivity = 100.0f;
    
    private Transform playerBody;

    float xRotation = 0.0f;
    private void Awake()
    {
        playerBody = transform.parent.GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        playerBody.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -89.9f, 89.9f);

        transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
    }
}
