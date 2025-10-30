using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;        // How fast the player moves
    private Rigidbody playerRb;       // Reference to the player's Rigidbody
    private GameObject focalPoint;    // Reference to the focal point for camera direction
    private float forwardInput;       // Input from player (W/S or Up/Down keys)
    public bool hasPowerup = false;
    private float powerUpStrength = 15.0f;
    public GameObject powerupIndicator;

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
        Debug.Log("Start");
        powerupIndicator.transform.position = transform.position + new Vector3(0,-0.5f,0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);

    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter hasPowerup=" + hasPowerup);
        
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            Debug.Log("Collided with:" + collision.gameObject.name + "with powerupset to" + hasPowerup);
            enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
        }
    }
}
