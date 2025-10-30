using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyX : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private GameObject playerGoal;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();

        // ✅ Assign reference to Player Goal object in the scene
        playerGoal = GameObject.Find("Player Goal");
    }

    // Update is called once per frame
    void Update()
    {
        // ✅ Only move if playerGoal exists
        if (playerGoal != null)
        {
            Vector3 lookDirection = (playerGoal.transform.position - transform.position).normalized;
            enemyRb.AddForce(lookDirection * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        if (other.gameObject.name == "Enemy Goal")
        {
            Destroy(gameObject);
        } 
        else if (other.gameObject.name == "Player Goal")
        {
            Destroy(gameObject);
        }
    }
}
