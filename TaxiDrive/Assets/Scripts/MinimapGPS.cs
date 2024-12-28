using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MinimapGPS : MonoBehaviour
{
    [SerializeField] private Transform player; // Reference to the player's transform
    [SerializeField] private LineRenderer lineRenderer; // Reference to the LineRenderer
    [SerializeField] private float pathHeightOffset = 20f; // Offset to align the path with the minimap view
    [SerializeField] private float pathUpdateSpeed = 0.1f; // How frequently the path updates

    private GameObject activeTarget; // Reference to the dynamically spawned target
    private NavMeshPath path; // Store the calculated path
    private Coroutine drawPathCoroutine; // Coroutine for drawing the path

    void Start()
    {
        path = new NavMeshPath();

        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // Set the layer for the LineRenderer's GameObject to the layer
        gameObject.layer = LayerMask.NameToLayer("Minimap");

        lineRenderer.positionCount = 0; // Start with no positions
        lineRenderer.startWidth = 0.2f; // Adjust width to fit the minimap
        lineRenderer.endWidth = 0.2f;
        lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        lineRenderer.material.color = Color.green; // Green GPS line

        // Start checking for the target
        StartCoroutine(FindTargetAndDrawPath());
    }

    private IEnumerator FindTargetAndDrawPath()
    {
        while (true) // Keep checking for a target indefinitely
        {
            // Find the target by tag ("Target")
            if (activeTarget == null)
            {
                activeTarget = GameObject.FindGameObjectWithTag("Target");

                if (activeTarget != null)
                {
                    // Start the path drawing once the target is found
                    if (drawPathCoroutine != null)
                    {
                        StopCoroutine(drawPathCoroutine);
                    }
                    drawPathCoroutine = StartCoroutine(DrawPathToTarget());
                }
            }

            // If the target is destroyed, reset the active target and start searching for a new one
            if (activeTarget == null && drawPathCoroutine != null)
            {
                StopCoroutine(drawPathCoroutine);
                lineRenderer.positionCount = 0; // Clear the path when the target is destroyed
            }

            yield return new WaitForSeconds(0.5f); // Check for the target every 0.5 seconds
        }
    }

    private IEnumerator DrawPathToTarget()
    {
        WaitForSeconds wait = new WaitForSeconds(pathUpdateSpeed);

        while (activeTarget != null)
        {
            // Calculate path using NavMesh
            if (NavMesh.CalculatePath(player.position, activeTarget.transform.position, NavMesh.AllAreas, path))
            {
                lineRenderer.positionCount = path.corners.Length;

                for (int i = 0; i < path.corners.Length; i++)
                {
                    // Set the path positions to the corners of the NavMesh path with a height offset for the minimap
                    lineRenderer.SetPosition(i, path.corners[i] + Vector3.up * pathHeightOffset);
                }
            }
            else
            {
                Debug.LogError($"Unable to calculate a path on the NavMesh between {player.position} and {activeTarget.transform.position}!");
            }

            yield return wait;
        }
    }
}