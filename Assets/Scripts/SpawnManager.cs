using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    private int animalIndex;
    public float spawnRangeX = 15;

    // Step 2.2: add delay before first spawn
    public float spawnDelay = 1f;
    public float spawnInterval = 1f;

    void Start()
    {
        // Step 2.1: call invoke repeat to make SpawnRandomAnimal run in specific interval
        // InvokeRepeating("SpawnRandomAnimal", 1, 1f);
        // InvokeRepeating(nameof(SpawnRandomAnimal), 1, 1f);

        // Step 2.2: add delay before first spawn
        InvokeRepeating(nameof(SpawnRandomAnimal), spawnDelay, spawnInterval);
    }

    void Update()
    {
        // Step 1.1: remove this 
        // if (Input.GetKeyDown(KeyCode.S))
        // {
        //     animalIndex = Random.Range(0, animalPrefabs.Length);
        //     Vector3 spawnPos = new(
        //         Random.Range(-spawnRangeX, spawnRangeX),
        //         transform.position.y,
        //         transform.position.z
        //     );
        //     Instantiate(
        //         animalPrefabs[animalIndex],
        //         spawnPos,
        //         animalPrefabs[animalIndex].transform.rotation
        //     );
        // }
    }

    // Step 1.2: create SpawnRandomAnimal method
    private void SpawnRandomAnimal()
    {
        animalIndex = Random.Range(0, animalPrefabs.Length);
        Vector3 spawnPos = new(
            Random.Range(-spawnRangeX, spawnRangeX),
            transform.position.y,
            transform.position.z
        );
        Instantiate(
            animalPrefabs[animalIndex],
            spawnPos,
            animalPrefabs[animalIndex].transform.rotation
        );
    }
}
