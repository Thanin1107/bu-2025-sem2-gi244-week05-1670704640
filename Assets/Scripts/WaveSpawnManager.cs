using UnityEngine;

public class WaveSpawnManager : MonoBehaviour
{
    // Step 5.4.1: add properties and states
    public Wave[] waveConfigurations;
    public WaveController waveController;
    private int currentWave = 0;
    private float waveEndTime = 0f;

    // Step 5.4.2: add Start method
    void Start()
    {
        waveController.StartWave(waveConfigurations[currentWave]);
        waveEndTime = Time.time + waveConfigurations[currentWave].waveInterval;
    }

    // Step 5.4.4: add Update method
    // NOTE: before this step, in 5.4.3, it requires to add IsComplete() to WaveController
    void Update()
    {
        if (currentWave >= waveConfigurations.Length)
            return;

        if (Time.time >= waveEndTime && waveController.IsComplete())
        {
            currentWave++;
            if (currentWave >= waveConfigurations.Length)
            {
                Debug.Log("All waves completed!");
            }
            else
            {
                waveController.StartWave(waveConfigurations[currentWave]);
                waveEndTime = Time.time + waveConfigurations[currentWave].waveInterval;
                Debug.Log($"Wave {currentWave + 1} started!");
            }
        }
    }
}