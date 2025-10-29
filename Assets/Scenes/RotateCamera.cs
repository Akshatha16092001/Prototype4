using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float rotationSpeed = 100.0f;
    private float horizontalInput;

    void Update()
    {
        // Get horizontal input from the player (A/D or Left/Right Arrow keys)
        horizontalInput = Input.GetAxis("Horizontal");

        // Rotate the focal point around the Y-axis
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
    }
}
