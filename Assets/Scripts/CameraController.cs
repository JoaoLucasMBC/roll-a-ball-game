using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float rotationSpeed = 5f;

    private Vector3 offset;
    private float currentAngleY;

    void Start()
    {
        offset = transform.position - player.transform.position;
        currentAngleY = transform.eulerAngles.y;
    }

    void LateUpdate()
    {
        // Mouse input for rotation
        currentAngleY += Input.GetAxis("Mouse X") * rotationSpeed;

        // Set the angle into a rotation around the Y axis (up axis)
        Quaternion rotation = Quaternion.Euler(0, currentAngleY, 0);

        // Adjust the camera position with the new rotation while maintaining the original offset distance
        transform.position = player.transform.position + rotation * offset;

        // Look at the player but keeps the camera's up vector parallel to the world's up vector
        transform.LookAt(player.transform.position + Vector3.up);
    }
}
