using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraKeys : MonoBehaviour
{
    public float cameraSpeed = 10; // Determines how fast the camera moves while pressing keys
    public float cameraMinimumX = 0; // Determines how far left the camera can move
    public float cameraMaximumX = 50; // Determines how far right the camera can move

    void Update()
    {
        // Get input
        float horizontalInput = Input.GetAxis("Horizontal");


        // Move camera
        transform.Translate(new Vector3(horizontalInput, 0, 0) * cameraSpeed * Time.deltaTime);


        // If camera is farther left than is allowed...
        if (transform.position.x < cameraMinimumX)
        {
            // Set x position to minimum
            transform.position = new Vector3(cameraMinimumX, transform.position.y, transform.position.z);
        }


        // If camera is farther right than is allowed...
        if (transform.position.x > cameraMaximumX)
        {
            // Set x position to maximum
            transform.position = new Vector3(cameraMaximumX, transform.position.y, transform.position.z);
        }
    }
}
