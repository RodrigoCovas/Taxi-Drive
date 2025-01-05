using UnityEngine;
using System.Collections;

public class SphereSpawner : MonoBehaviour
{
    public GameObject startSpherePrefab;
    public GameObject endSpherePrefab;
    public RandomRoadPoint randomRoadPoint;
    public RandomRoadPoint.SpawnMode startSpawnMode = RandomRoadPoint.SpawnMode.NearPlayer;
    public RandomRoadPoint.SpawnMode endSpawnMode = RandomRoadPoint.SpawnMode.Random;

    public EventTimer timer;
    public float challengeTime = 60f;
    public PlayerComfort comfortBar;

    private void Start()
    {
        randomRoadPoint = FindObjectOfType<RandomRoadPoint>();
        if (randomRoadPoint == null)
        {
            Debug.LogError("RandomRoadPoint script not found in the scene!");
            return;
        }
        StartCoroutine(SpawnSpheresSequentially());
    }

    private IEnumerator SpawnSpheresSequentially()
    {
        // Spawn the start sphere
        Vector3 startSpawnPosition = randomRoadPoint.GetRandomRoadPoint(startSpawnMode);
        if (startSpawnPosition != Vector3.zero)
        {
            GameObject startSphere = Instantiate(startSpherePrefab, startSpawnPosition, Quaternion.identity);
            startSphere.tag = "Target";

            TimerSphereTrigger startTrigger = startSphere.GetComponent<TimerSphereTrigger>();
            if (startTrigger != null)
            {
                startTrigger.sphereToControl = startSphere;
                startTrigger.destructionTime = 3.0f; // Set destruction time for the start sphere
                startTrigger.AssignTimerComfort(timer, challengeTime, comfortBar);
            }

            // Wait until the start sphere is destroyed
            yield return new WaitUntil(() => startSphere == null);
        }
        else
        {
            Debug.LogWarning("Suitable start point not found!");
            yield break; // Exit the coroutine if no start point is found
        }

        // Spawn the end sphere
        Vector3 endSpawnPosition = randomRoadPoint.GetRandomRoadPoint(endSpawnMode);
        if (endSpawnPosition != Vector3.zero)
        {
            GameObject endSphere = Instantiate(endSpherePrefab, endSpawnPosition, Quaternion.identity);
            endSphere.tag = "Target";

            TimerSphereTrigger endTrigger = endSphere.GetComponent<TimerSphereTrigger>();
            if (endTrigger != null)
            {
                endTrigger.sphereToControl = endSphere;
                endTrigger.destructionTime = 5.0f; // Set destruction time for the end sphere
                endTrigger.AssignTimerComfort(timer, challengeTime, comfortBar);
            }
        }
        else
        {
            Debug.LogWarning("Suitable end point not found!");
        }
    }
}
