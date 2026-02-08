
using UnityEngine;

// Step 5.2: make Wave class serializable
// and add enemy prefabs, enemy count, spawn interval, wave interval
[System.Serializable]
public class Wave
{
    public GameObject[] enemyPrefabs;
    public int enemyCount;
    public float spawnInterval = 1.5f;
    public float waveInterval = 5f;
}