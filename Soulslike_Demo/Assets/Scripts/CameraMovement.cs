using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform mainCamera;
    public float raycastDistance = 5f;
    public LayerMask ignoreLayer;

    public bool IsCameraLookingAtBack()
    {
        // Ensure camera is assigned
        if (mainCamera == null)
        {
            mainCamera = Camera.main.transform;
        }

        // Create ray from camera towards player
        Ray ray = new Ray(mainCamera.position, transform.position - mainCamera.position);

        // Raycast to check if camera has clear line of sight to player's back
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit))
        {
            // Check if hit is the player and roughly behind
            if (hit.collider.transform == transform)
            {
                // Calculate angle between camera forward and player's back
                float angleToBack = Vector3.Angle(mainCamera.forward, -transform.forward);

                // Return true if camera is looking close to back (e.g., within 45 degrees)
                return angleToBack < 45f;

                /*
                // Visualize ray in the scene view for debugging
                Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.green);
                return true;
                */
            }
        }

        // Visualize ray in the scene view for debugging
        // Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red);
        return false;
    }
}
