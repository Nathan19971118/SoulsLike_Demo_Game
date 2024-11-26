using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float rotationSpeed = 7f;
    public float jumpForce;

    [Header("Ground Check")]
    public bool isGrounded;

    [Header("Dodge")]
    public float dodgeForce;
    public float dodgeCooldown = 0.5f;
    bool isDodge = false;

    [Header("References")]
    public Transform orientation;
    public Transform mainCamera;

    [Header("Input")]
    float horizontalInput;
    float verticalInput;
    float camHorizontalInput;
    float camVerticalInput;
    bool jumpInput;
    bool dodgeInput;

    [HideInInspector]
    public AnimatorHandler animatorHandler;
    public Rigidbody rigidBody;
    float dodgeTimer;


    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animatorHandler=GetComponentInChildren<AnimatorHandler>();
        rigidBody.freezeRotation = true;
        isGrounded = true;

        // If mainCamera is not set in inspector, try to find it
        if (mainCamera == null)
            mainCamera = Camera.main.transform;
    }

    private void Update()
    {
        GetUserInput();

        if (jumpInput && isGrounded)
        {
            Jump();
        }

        if (dodgeInput && dodgeTimer <= 0)
        {
            Dodge();
        }

        if (dodgeTimer > 0)
        {
            dodgeTimer -= Time.deltaTime;
        } 
    }

    private void FixedUpdate()
    {
        if (!isDodge)
        {
            HandleMovementAndRotation();
        }
    }

    private void GetUserInput()
    {
        // Get Input Axis
        horizontalInput = Input.GetAxis("MoveHorizontal");
        verticalInput = Input.GetAxis("MoveVertical");
        camHorizontalInput = Input.GetAxis("CameraMoveHorizontal");
        camVerticalInput = Input.GetAxis("CameraMoveVertical");

        // Get jump Input
        jumpInput = Input.GetButtonDown("Jump");

        // Get dodge Input
        dodgeInput = Input.GetButtonDown("Dodge");

        // Update animator values
        animatorHandler.UpdateAnimatorValues(verticalInput, horizontalInput);

    }

    public void HandleMovementAndRotation()
    {
        if (animatorHandler.canRotate)
        {
            if (camHorizontalInput == 0)
            {
                // Only process if there's input
                if (horizontalInput != 0 || verticalInput != 0)
                {
                    // Calculate the input direction
                    Vector3 inputDirection = new Vector3(horizontalInput, 0, verticalInput);
                    float inputMagnitude = inputDirection.magnitude;

                    // Normalize the direction and handle deadzone
                    if (inputMagnitude > 0.19f) // Small deadzone to prevent drift
                    {
                        // Calculate target rotation from input
                        float targetAngle = Mathf.Atan2(horizontalInput, verticalInput) * Mathf.Rad2Deg;
                        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

                        // Smoothly rotate to back movement direction
                        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                        // Move forward in the direction we're facing
                        transform.position += transform.forward * moveSpeed * inputMagnitude * Time.deltaTime;
                    }
                }
            }

            else if (camHorizontalInput != 0)
            {

            }
            
        }
    }

    private void Jump()
    {
        if (jumpInput && isGrounded && !isDodge)
        {
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animatorHandler.HandleJumpAnim();
            isGrounded = false;
        }
    }

    private void Dodge()
    {
        if (dodgeInput && !isDodge)
        {
            Vector3 dodgeDir = new Vector3(horizontalInput, 0, verticalInput).normalized;

            if (dodgeDir.magnitude == 0)
            {
                dodgeDir = transform.forward;
            }
            rigidBody.AddForce(dodgeDir * dodgeForce, ForceMode.Impulse);
            animatorHandler.HandleDodgeAnim();
            isDodge = true;
        }

        dodgeTimer = dodgeCooldown;

        // Reset isDodge after a short delay
        Invoke(nameof(ResetDodge), dodgeCooldown);
    }

    private void ResetDodge()
    {
        isDodge = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
