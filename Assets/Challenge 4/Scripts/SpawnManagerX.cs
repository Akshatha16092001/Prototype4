using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    public GameObject player;

    private float spawnRangeX = 10;
    private float spawnZMin = 15;
    private float spawnZMax = 25;

    public int enemyCount;
    public int waveCount = 1;

    void Update()
    {
        // ✅ Count all active enemies
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        // ✅ When all enemies are gone, spawn the next wave
        if (enemyCount == 0)
        {
            SpawnEnemyWave(waveCount);
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        Vector3 powerupSpawnOffset = new Vector3(0, 0, -15);

        // ✅ Spawn powerup if none exist
        if (GameObject.FindGameObjectsWithTag("Powerup").Length == 0)
        {
            Instantiate(powerupPrefab, GenerateSpawnPosition() + powerupSpawnOffset, powerupPrefab.transform.rotation);
        }

        // ✅ Use enemiesToSpawn parameter to control enemy count
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }

        // ✅ Increase wave count for next round
        waveCount++;

        // ✅ Reset player position
        ResetPlayerPosition();
    }

    Vector3 GenerateSpawnPosition()
    {
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = Random.Range(spawnZMin, spawnZMax);
        return new Vector3(xPos, 0, zPos);
    }

    void ResetPlayerPosition()
    {
        player.transform.position = new Vector3(0, 1, -7);
        Rigidbody playerRb = player.GetComponent<Rigidbody>();
        playerRb.velocity = Vector3.zero;
        playerRb.angularVelocity = Vector3.zero;
    }
}
