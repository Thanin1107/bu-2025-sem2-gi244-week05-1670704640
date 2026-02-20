using UnityEngine;

public class WaveSpawnManager : MonoBehaviour
{
    public Wave[] waves;
    public WaveController waveController;
    private int currnWave;
    void Start()
    {
        currnWave = 0;
        waveController.ChangWave(waves[0]);
    }

    void Update()
    {
        if (waveController.Iscompleted())
        {
            currnWave++;
            if (currnWave == waves.Length)
            {
                waveController.ChangWave(waves[currnWave]);
            }
        }
    }
}