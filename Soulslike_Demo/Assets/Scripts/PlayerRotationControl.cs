using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotationControl : MonoBehaviour
{
    [Header("References")]
    public Rigidbody playerRigidBody;
    public Transform orientation;

    [Header("Rotation Settings")]
    public float rotationSpeed = 10f;
    public float deadZone = 0.1f;

    PlayerMovement playerMovement;

    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get left stick movement input
        float horizontalInput = Input.GetAxis("MoveHorizontal");
        float verticalInput = Input.GetAxis("MoveVertical");

        // Get right stick input for camera/rotation direction
        float camHorizontal = Input.GetAxis("CameraMoveHorizontal");
        float camvertical = Input.GetAxis("CameraMoveVertical");

        // Determine rotation input
        Vector3 rotationInput = new Vector3(camHorizontal, 0, camvertical);
        Vector3 movementInput = new Vector3(horizontalInput, 0, verticalInput);

        // Check if there's significant input from right stick
        if (rotationInput.magnitude > deadZone)
        {
            // Rotate based on right stick input
            Quaternion targetRotation = Quaternion.LookRotation(rotationInput);
            playerRigidBody.rotation = Quaternion.Slerp(playerRigidBody.rotation, 
                targetRotation, rotationSpeed * Time.deltaTime);
        }

        // If no right stick input, use movement direction
        else if (movementInput.magnitude > deadZone)
        {
            // Rotate based on movement input
            playerMovement.HandleMovementAndRotation();
        }
    }
}
