1.1 `SpawnManager.cs` remove keyDown S to spawn animal

---

1.2 move spawning animation create new method for spawning animal

```c#
void SpawnRandomAnimal() {
    int animalIndex = Random.Range(0, animalPrefabs.Length);
    Vector3 spawnpos = new Vector3(Random.Range(-xSpawnRange,
    xSpawnRange), 0, zSpawnPos);
    Instantiate(animalPrefabs[animalIndex], new Vector3(0, 0, 20) spawnpos,
    animalPrefabs[animalIndex].transform.rotation);
}
```

---

2.1 spawn animal at the specific of time, instead of using method name as string literally
we can use `nameof` to make code more maintainable (we can rename function without worrying)

```c#
// InvokeRepeating("SpawnRandomAnimal", 1, 1f);
    InvokeRepeating(nameof(SpawnRandomAnimal), 1, 1f);
```

then let's see, how it works, try to change

- time (2nd parameter): like delay
- repeatRate (3rd parameter): interval

---

2.2 refractor by declare `spawnDelay` and `spawnInterval`

```c#
    void Start()
    {
        // Step 2.2: add delay before first spawn
        InvokeRepeating(nameof(SpawnRandomAnimal), spawnDelay, spawnInterval);
    }
```

3.1 edit Collider in Dog[XX] prefab.

- using "Edit Collider" in Collider component this is way better than set `center` and `size` and using isometric view than perspective
- and set `IsTrigger` to `true`.

---

3.2 add rigidbody component to `Food` prefab.

- this makes collision detection works between 2 objects, atleast one object need rigidbody component.
- Dont forget to UNCHECK `Use gravity`

---

3.3 Make Health Component

Add `HealthV1` onto `DogXX` prefab, and in script `HealthV1` update code

```
    // Step 3.1: add health variable
    public int health = 100;

    public void TakeDamage(int damage)
    {
        // Step 3.2: reduce health and destroy game object if health is less than or equal to 0
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
```

---

3.4 Add `Food.cs` onto `Food` prefab. Explain about How Trigger works
In InTriggerEnter, implements

```
    void OnTriggerEnter(Collider other)
    {
        var health = other.GetComponent<HealthV1>();
        if (health != null)
        {
            health.TakeDamage(attackPoint);
        }
        Destroy(gameObject);
    }
```

---

3.5 Refractor to use TryGetComponent to reduce memory allocation

```
    void OnTriggerEnter2(Collider other)
    {
        if (other.TryGetComponent(out HealthV1 health))
        {
            health.TakeDamage(attackPoint);
        }
        Destroy(gameObject);
    }
```

---

3.6 Introduce `HealthV2` which is instead of deduct health by the taken damage
we will store accumulate damage and compare its value with `maxHealth`,
if the accumulate damage is over the health, it means object should die

```
    public int maxHealth = 100;
    public int accumDamage = 0;

    public void TakeDamage(int damage)
    {
        accumDamage += damage;
        if (accumDamage >= maxHealth)
        {
            Destroy(gameObject);
        }
    }
```

this way, we can reset damage by setting `accumDamage` to 0,
or when player got power-up item that increase maxHealth,
we do not need to recompute the remaining HP compare to `HealthV1`
if we need to manage more logic to accomplish max health power-up requirement

---

4.1 Control the Game state (`GameState.cs`), explain that the game state determine when player lost the game
if he left animal to pass by the frontier (go to hit the specific trigger). In the example,
we allow just only 4 animals pass by, the 5th animal pass by, GAME OVER

we need to create a new Tag `"Animal"` and add it to all enemy prefabs.

```
    private int hitCount = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animal"))
        {
            hitCount++;
            if (hitCount >= 5)
            {
                Debug.Log("Game Over!");
                Time.timeScale = 0;
            }
        }
    }
```

This script works with Trigger, then we need to place the Empty GameObject with Specific trigger
which cover possible area.

---

5.1 Introduce Wave System
Wave system composes of 3 parts

- Wave Data (`Wave.cs`)
- Wave Controller (`WaveController.cs`)
- Wave Spawner (`WaveSpawnManager.cs`)

Dont forget to disable `SpawnManager` GameObject.

---

5.2 Implement Wave Data

Explain data + Serializable

```c#
[System.Serializable]
public class Wave
{
    public GameObject[] enemyPrefabs;
    public int enemyCount;
    public float spawnInterval = 1.5f;
    public float waveInterval = 5f;
}
```

5.3 Implement Wave Controller

5.3.1 add WaveController properties and its states. Need to explain each variable

```c#
    public Transform[] spawnPoints;

    private Wave currentWave;
    private int enemiesSpawned = 0;
    private float nextSpawnTime = 0f;
```

---

5.3.2 add StartWave(...) method for starting the wave

```c#
    public void StartWave(Wave wave)
    {
        currentWave = wave;
        enemiesSpawned = 0;
        nextSpawnTime = Time.time;
    }
```

---

5.3.3 add SpawnEnemy() method

```c#
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
```

---

5.3.4 add Update() method

```c#
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
```

5.4 Inroduce to WaveSpawnManager
SpawnManager writes up both wave data and wave controller,

5.4.1 add WaveSpawnManager properties and its states. Need to explain each variable

```c#
    public Wave[] waveConfigurations;
    public WaveController waveController;

    private int currentWave = 0;
    private float waveEndTime = 0f;
```

---

5.4.2 add Start() method

```c#
    void Start()
    {
        waveController.StartWave(waveConfigurations[currentWave]);
        waveEndTime = Time.time + waveConfigurations[currentWave].waveInterval;
    }
```

---

5.4.3 add IsComplete() method in `WaveController.cs`

```c#
    public bool IsComplete()
    {
        return enemiesSpawned >= currentWave?.enemyCount;
    }
```

---

5.4.4 add update method

```c#
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
```
