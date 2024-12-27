using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    public GameObject spherePrefab;
    public int numberOfSpheres = 1;
    public RandomRoadPoint randomRoadPoint;
    public RandomRoadPoint.SpawnMode spawnMode = RandomRoadPoint.SpawnMode.Random;

    private void Start()
    {
        randomRoadPoint = FindObjectOfType<RandomRoadPoint>();
        if (randomRoadPoint == null)
        {
            Debug.LogError("RandomRoadPoint script not found in the scene!");
            return;
        }

        SpawnSpheres();
    }

    private void SpawnSpheres()
    {
        for (int i = 0; i < numberOfSpheres; i++)
        {
            Vector3 spawnPosition = randomRoadPoint.GetRandomRoadPoint(spawnMode);
            if (spawnPosition != Vector3.zero)
            {
                Instantiate(spherePrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("No suitable point found!");
            }
        }
    }
}
