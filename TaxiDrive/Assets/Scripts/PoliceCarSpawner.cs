using UnityEngine;
using System.Collections.Generic;

public class PoliceCarSpawner : MonoBehaviour
{
    public GameObject policeCarPrefab;
    public RandomRoadPoint randomRoadPoint;
    public Transform player;
    public RandomRoadPoint.SpawnMode spawnMode = RandomRoadPoint.SpawnMode.NearPlayer;
    public int maxPoliceCars;
    
    private Transform spawnParent;
    private List<GameObject> spawnedPoliceCars = new List<GameObject>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnPoliceCar();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            DespawnRandomPoliceCar();
        }
    }

    public void SpawnPoliceCar()
    {
        if (policeCarPrefab == null || randomRoadPoint == null || player == null)
        {
            Debug.LogError("Police car prefab, RandomRoadPoint script, or Player Transform is not assigned!");
            return;
        }

        if (spawnedPoliceCars.Count >= maxPoliceCars)
        {
            Debug.LogWarning("Maximum number of police cars reached.");
            return;
        }

        Vector3 spawnPoint = randomRoadPoint.GetRandomRoadPoint(spawnMode);

        if (spawnPoint == Vector3.zero)
        {
            Debug.LogWarning("Failed to find a valid spawn point for the police car.");
            return;
        }

        GameObject newPoliceCar = Instantiate(policeCarPrefab, spawnPoint, Quaternion.identity, spawnParent);
        newPoliceCar.GetComponent<AIController>().SetPlayerTransform(player);
        spawnedPoliceCars.Add(newPoliceCar);

        Debug.Log($"Police car spawned at {spawnPoint}. Total: {spawnedPoliceCars.Count}");
    }

    public void DespawnRandomPoliceCar()
    {
        if (spawnedPoliceCars.Count == 0)
        {
            Debug.LogWarning("No police cars available to despawn.");
            return;
        }

        int randomIndex = Random.Range(0, spawnedPoliceCars.Count);
        GameObject policeCarToDespawn = spawnedPoliceCars[randomIndex];

        spawnedPoliceCars.RemoveAt(randomIndex);
        Destroy(policeCarToDespawn);

        Debug.Log($"Police car despawned. Remaining: {spawnedPoliceCars.Count}");
    }
}
