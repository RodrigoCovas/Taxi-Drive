using UnityEngine;
using UnityEngine.AI;

public class RandomRoadPoint : MonoBehaviour
{
    [SerializeField] private MeshCollider roadMeshCollider; // Mesh collider of the road
    [SerializeField] private Transform playerTransform; // Reference to the player
    [SerializeField] private float nearPlayerRadius = 300f; // Radius around the player to spawn objects
    [SerializeField] private float minPlayerDistance = 50f; // Minimum distance from the player for spawning
    [SerializeField] private float maxAttempts = 1000f; // Maximum attempts to find a valid point
    [SerializeField] private float sampleRadius = 5f; // The radius for finding the nearest walkable point

    public Vector3 GetRandomRoadPoint(SpawnMode mode)
    {
        if (roadMeshCollider == null || playerTransform == null)
        {
            Debug.LogError("Road mesh collider or player transform is not assigned!");
            return Vector3.zero;
        }

        if (mode == SpawnMode.NearPlayer)
        {
            return GetNearPlayerPoint();
        }
        else
        {
            return GetRandomPoint();
        }
    }

    private Vector3 GetNearPlayerPoint()
    {
        for (int i = 0; i < maxAttempts; i++)
        {
            // Generate a random point within the near-player radius
            Vector3 randomPoint = GetRandomPointAroundPlayer();

            // Find the nearest walkable point using NavMesh.SamplePosition
            Vector3 nearestWalkablePoint = GetNearestWalkablePoint(randomPoint);

            if (nearestWalkablePoint != Vector3.zero)
            {
                return nearestWalkablePoint;
            }
        }

        Debug.LogWarning("Maximum attempts reached. No suitable near-player point found.");
        return Vector3.zero;
    }

    private Vector3 GetRandomPoint()
    {
        for (int attempts = 0; attempts < maxAttempts; attempts++)
        {
            Bounds bounds = roadMeshCollider.sharedMesh.bounds; // Get bounds of the road mesh

            // Generate a random point within the bounds of the road mesh
            Vector3 randomPoint = bounds.center + new Vector3(
                Random.Range(-bounds.extents.x, bounds.extents.x),
                Random.Range(-bounds.extents.y, bounds.extents.y),
                Random.Range(-bounds.extents.z, bounds.extents.z)
            );

            // Find the nearest walkable point using NavMesh.SamplePosition
            Vector3 nearestWalkablePoint = GetNearestWalkablePoint(randomPoint);

            if (nearestWalkablePoint != Vector3.zero)
            {
                return nearestWalkablePoint;
            }
        }

        Debug.LogWarning("Maximum attempts reached. No suitable point found.");
        return Vector3.zero;
    }

    private Vector3 GetRandomPointAroundPlayer()
    {
        // Generate a random point around the player within a defined radius
        Vector3 playerPosition = playerTransform.position;

        float randomDistance = Random.Range(minPlayerDistance, nearPlayerRadius);
        float randomAngle = Random.Range(0f, 360f);

        Vector3 offset = new Vector3(
            Mathf.Cos(randomAngle * Mathf.Deg2Rad) * randomDistance,
            0,
            Mathf.Sin(randomAngle * Mathf.Deg2Rad) * randomDistance
        );

        return playerPosition + offset;
    }

    private Vector3 GetNearestWalkablePoint(Vector3 point)
    {
        NavMeshHit hit;

        // Use NavMesh.SamplePosition to find the closest walkable point to the given point
        if (NavMesh.SamplePosition(point, out hit, sampleRadius, NavMesh.AllAreas))
        {
            // Return the nearest valid point on the NavMesh
            return hit.position;
        }

        // If no valid point is found, return Vector3.zero
        return Vector3.zero;
    }

    public enum SpawnMode
    {
        Random,
        NearPlayer
    }
}
