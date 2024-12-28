using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player;
    public float xOffset = 11f;

    private void LateUpdate()
    {
        // Get the player's position and rotation
        Vector3 playerPosition = player.position;
        float playerRotationY = player.eulerAngles.y;

        // Calculate the offset based on the player's rotation
        Vector3 offset = new Vector3(Mathf.Sin(playerRotationY * Mathf.Deg2Rad) * xOffset,
                                      0f,
                                      Mathf.Cos(playerRotationY * Mathf.Deg2Rad) * xOffset);

        // Update the camera's position with the new offset while keeping the y value constant
        Vector3 newPosition = playerPosition + offset;
        newPosition.y = transform.position.y;  // Keep the camera's y position constant

        // Apply the new position and rotation to the camera
        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(90f, playerRotationY, 0f);
    }
}
