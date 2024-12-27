using UnityEngine;

public class RandomRoadPoint : MonoBehaviour
{
    public enum SpawnMode { Random, NearPlayer }

    public MeshCollider roadMeshCollider;
    public Transform playerTransform;
    public float nearPlayerRadius = 300f;
    public float minPlayerDistance = 50f;
    public int maxAttempts = 1000;

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

            if (ValidatePointOnRoad(randomPoint))
            {
                //Debug.Log(Vector3.Distance(playerTransform.position, randomPoint));
                return randomPoint;
            }
        }

        Debug.LogWarning("Maximum attempts reached. No suitable near-player point found.");
        return Vector3.zero;
    }

    private Vector3 GetRandomPoint()
    {
        int attempts = 0;
        while (attempts < maxAttempts)
        {
            Bounds bounds = roadMeshCollider.sharedMesh.bounds; // Use sharedMesh to avoid potential errors.

            Vector3 randomPoint = bounds.center + new Vector3(
                Random.Range(-bounds.extents.x, bounds.extents.x),
                Random.Range(-bounds.extents.y, bounds.extents.y),
                Random.Range(-bounds.extents.z, bounds.extents.z)
            );

            RaycastHit hit;
            // Perform a raycast to check if the point intersects with the mesh
            if (Physics.Raycast(randomPoint + Vector3.up * 0.1f, Vector3.down, out hit, 10f))
            {
                if (hit.collider != null && hit.collider.gameObject == roadMeshCollider.gameObject)
                {
                    //Debug.Log("Point found after " + attempts + " attempts.");
                    return hit.point;
                }
            }

            attempts++;
        }

        Debug.LogWarning("Maximum attempts reached. No suitable point found.");
        return Vector3.zero;
    }

    private Vector3 GetRandomPointAroundPlayer()
    {
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

    private bool ValidatePointOnRoad(Vector3 point)
    {
        if (Physics.Raycast(point + Vector3.up * 10f, Vector3.down, out RaycastHit hit, 20f))
        {
            return hit.collider != null && hit.collider == roadMeshCollider;
        }

        return false;
    }
}
