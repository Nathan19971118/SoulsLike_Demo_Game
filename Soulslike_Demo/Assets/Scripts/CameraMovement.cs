using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Input")]
    float camHorizontalInput;
    float camVerticalInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetCameraMovementInput()
    {
        camHorizontalInput = Input.GetAxis("CameraMoveHorizontal");
        camVerticalInput = Input.GetAxis("CameraMoveVertical");
    }
}
