using UnityEngine;

public class EventSpawner : MonoBehaviour
{
    public PoliceCarSpawner policeCarSpawner;
    public BuffSpawner buffSpawner;

    public float minSpawnInterval = 1f;
    public float maxSpawnInterval = 3f;

    private float nextSpawnTime;

    void Start()
    {
        ScheduleNextSpawn();
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnObjects();
            ScheduleNextSpawn();
        }
    }

    private void SpawnObjects()
    {
        if (Random.value > 0.05f)
        {
            buffSpawner.SpawnBuff();
            Debug.Log("Buff spawned");
        }
        else
        {
            policeCarSpawner.SpawnPoliceCar();
            Debug.Log("Police car spawned");
        }
    }

    private void ScheduleNextSpawn()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
        Debug.Log($"Next spawn scheduled in {nextSpawnTime - Time.time:F2} seconds.");
    }
}
