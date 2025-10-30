using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 8.0f;             // ⬅ Reduced movement speed
    public bool hasPowerup = false;
    private GameObject focalPoint;
    public float powerupStrength = 10.0f;  // ⬅ Slightly reduced knockback force
    public GameObject powerupIndicator;

    private float maxSpeed = 12.0f;        // ⬅ Max movement speed limit

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");

        // ✅ Use ForceMode.Force for smooth, physics-based acceleration
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput, ForceMode.Force);

        // ✅ Cap player speed (prevents runaway acceleration)
        if (playerRb.velocity.magnitude > maxSpeed)
        {
            playerRb.velocity = playerRb.velocity.normalized * maxSpeed;
        }

        // Keep indicator under the player
        if (powerupIndicator != null)
            powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            if (powerupIndicator != null)
                powerupIndicator.SetActive(true);

            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        if (powerupIndicator != null)
            powerupIndicator.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (enemyRigidbody != null)
            {
                // ✅ Push enemy *away* from player
                Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position).normalized;
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }

            Debug.Log("Hit enemy with powerup, sending it away!");
        }
    }
}
