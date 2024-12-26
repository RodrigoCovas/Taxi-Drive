using UnityEngine;
using System.Collections.Generic;

public class RandomRoadPoint : MonoBehaviour
{
    public List<MeshFilter> roadMeshes;
    public int maxAttempts = 1000; // Maximum number of attempts before giving up

    public Vector3 GetRandomRoadPoint()
    {
        return GetRandomRoadPointRecursive(0);
    }

    private Vector3 GetRandomRoadPointRecursive(int attempts)
    {
        if (roadMeshes == null || roadMeshes.Count == 0)
        {
            Debug.LogError("No road meshes assigned!");
            return Vector3.zero;
        }

        MeshFilter selectedMesh = roadMeshes[Random.Range(0, roadMeshes.Count)];
        Bounds bounds = selectedMesh.sharedMesh.bounds; // Use sharedMesh to avoid potential errors.
        Vector3 randomPoint = bounds.center + new Vector3(
            Random.Range(-bounds.extents.x, bounds.extents.x),
            Random.Range(-bounds.extents.y, bounds.extents.y),
            Random.Range(-bounds.extents.z, bounds.extents.z)
        );

        RaycastHit hit;
        if (Physics.Raycast(randomPoint + Vector3.up * 0.1f, Vector3.down, out hit, 10f))
        {
            if (hit.collider != null && hit.collider.gameObject == selectedMesh.gameObject)
            {
                Debug.Log("Point found after " + attempts + " attempts.");
                return hit.point;
            }
        }

        if (attempts < maxAttempts)
        {
            return GetRandomRoadPointRecursive(attempts + 1);
        }
        else
        {
            Debug.LogWarning("Maximum attempts reached. No suitable point found.");
            return Vector3.zero;
        }
    }
}
