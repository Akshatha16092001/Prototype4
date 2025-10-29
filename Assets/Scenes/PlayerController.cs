using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;        // How fast the player moves
    private Rigidbody playerRb;       // Reference to the player's Rigidbody
    private GameObject focalPoint;    // Reference to the focal point for camera direction
    private float forwardInput;       // Input from player (W/S or Up/Down keys)

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        // Get the forward/backward input
        forwardInput = Input.GetAxis("Vertical");

        // Move the player in the direction the camera (focal point) is facing
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
    }
}
