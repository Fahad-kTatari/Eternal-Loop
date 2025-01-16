using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // Prefab of Enemy GameObject
    public float spawnIntervalLevel1 = 2f; // Spawn interval for Level 1
    public float spawnIntervalLevel2 = 1f; // Spawn interval for Level 2
    private float spawnInterval; // Current spawn interval
    private float spawnTimer; // Timer to keep track of how much time has elapsed

    public float fallSpeedLevel1 = 0.8f; // Fall speed for Level 1 enemies
    public float fallSpeedLevel2 = 2.2f; // Fall speed for Level 2 enemies

    void Start()
    {
        // Set the spawn interval based on the level
        UpdateSpawnInterval();
    }

    void Update()
    {
        // Increment the timer value
        spawnTimer += Time.deltaTime;

        // Spawn enemies if the game is not over and the timer exceeds the spawn interval
        if (!GameManager.instance.IsGameOver() && spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f; // Reset the timer
        }
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(-7f, 7f); // Random X position
        Vector2 spawnPosition = new Vector2(randomX, 6f);

        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Adjust the fall speed based on the level
        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            string currentLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

            if (currentLevel == "Level1")
            {
                rb.velocity = new Vector2(0, -fallSpeedLevel1); // Slow fall speed
            }
            else if (currentLevel == "Level2")
            {
                rb.velocity = new Vector2(0, -fallSpeedLevel2); // Faster fall speed
            }
        }
    }

    private void UpdateSpawnInterval()
    {
        string currentLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (currentLevel == "Level1")
        {
            spawnInterval = spawnIntervalLevel1; // Slower spawn interval for Level 1
        }
        else if (currentLevel == "Level2")
        {
            spawnInterval = spawnIntervalLevel2; // Faster spawn interval for Level 2
        }
    }
}
