using UnityEngine;

public class WaveController : MonoBehaviour
{
    // Step 5.3.1: add properties and states
    public Transform[] spawnPoints;
    private Wave currentWave;
    private int enemiesSpawned = 0;
    private float nextSpawnTime = 0f;

    // Step 5.3.2: add StartWave method
    public void StartWave(Wave wave)
    {
        currentWave = wave;
        enemiesSpawned = 0;
        nextSpawnTime = Time.time;
    }

    // Step 5.3.3: add SpawnEnemy method
    void SpawnEnemy()
    {
        int enemyIndex = Random.Range(0, currentWave.enemyPrefabs.Length);
        int spawnIndex = Random.Range(0, spawnPoints.Length);

        GameObject enemy = Instantiate(
            currentWave.enemyPrefabs[enemyIndex],
            spawnPoints[spawnIndex].position,
            spawnPoints[spawnIndex].rotation
        );
    }

    // Step 5.3.4: add Update method
    void Update()
    {
        if (currentWave == null) return;

        if (enemiesSpawned < currentWave.enemyCount && Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            enemiesSpawned++;
            nextSpawnTime = Time.time + currentWave.spawnInterval;
        }
    }

    // Step 5.4.3: add IsComplete method
    public bool IsComplete()
    {
        return enemiesSpawned >= currentWave?.enemyCount;
    }
}
