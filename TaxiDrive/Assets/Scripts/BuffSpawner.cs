using UnityEngine;
using System.Collections.Generic;

public class BuffSpawner : MonoBehaviour
{
    public GameObject clockPrefab;
    public GameObject drinkPrefab;
    public GameObject mirrorPrefab;
    public GameObject alarmPrefab;

    public RandomRoadPoint randomRoadPoint;
    public RandomRoadPoint.SpawnMode spawnMode = RandomRoadPoint.SpawnMode.NearPlayer;

    private Transform spawnParent;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SpawnBuff();
        }
    }

    public void SpawnBuff()
    {
        if (randomRoadPoint == null)
        {
            Debug.LogError("RandomRoadPoint script is not assigned!");
            return;
        }

        Vector3 spawnPoint = randomRoadPoint.GetRandomRoadPoint(spawnMode);

        if (spawnPoint == Vector3.zero)
        {
            Debug.LogWarning("Failed to find a valid spawn point for the object.");
            return;
        }

        spawnPoint.y += 1f;
        GameObject buffToSpawn = ChooseRandomBuff();
        Instantiate(buffToSpawn, spawnPoint, Quaternion.identity, spawnParent);

        Debug.Log($"Object spawned at {spawnPoint}.");
    }

    private GameObject ChooseRandomBuff()
    {
        int randomChoice = Random.Range(0, 4);

        switch (randomChoice)
        {
            case 0:
                return clockPrefab;
            case 1:
                return drinkPrefab;
            case 2:
                return mirrorPrefab;
            case 3:
                return alarmPrefab;
            default:
                return clockPrefab;
        }
    }
}
