using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinimapDistanceDisplay : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer; // Reference to the LineRenderer used for the path
    [SerializeField] private TextMeshProUGUI distanceText; // Reference to a TextMeshProUGUI to display the distance
    [SerializeField] private float heightOffset = 10f; // Offset to align with minimap visuals

    private float CalculatePathDistance()
    {
        if (lineRenderer == null || lineRenderer.positionCount < 2)
        {
            return 0f; // No valid path
        }

        float totalDistance = 0f;
        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(positions);

        for (int i = 1; i < positions.Length; i++)
        {
            // Calculate segment distance
            Vector3 adjustedStart = positions[i - 1] - Vector3.up * heightOffset;
            Vector3 adjustedEnd = positions[i] - Vector3.up * heightOffset;

            totalDistance += Vector3.Distance(adjustedStart, adjustedEnd);
        }

        return totalDistance;
    }

    private void Update()
    {
        float distance = CalculatePathDistance();
        if (distanceText != null)
        {
            // Update the displayed text
            distanceText.text = $"{distance:F2} m";
        }
    }
}
