using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    public GameObject spherePrefab;
    public int numberOfSpheres = 1;
    public RandomRoadPoint randomRoadPoint;

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
            Vector3 randomPosition = randomRoadPoint.GetRandomRoadPoint();
            if (randomPosition != Vector3.zero)
            {
                Instantiate(spherePrefab, randomPosition, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("No suitable point found!");
            }
        }
    }
}
