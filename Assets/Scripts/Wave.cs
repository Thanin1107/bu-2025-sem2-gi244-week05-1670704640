
using System;
using UnityEngine;
[Serializable]
public class Wave
{
    public GameObject[] enemyPrefabs;
    public int enemyCount;
    public float spawnInterval = 1f;
    public float waveInterval = 10f;
}