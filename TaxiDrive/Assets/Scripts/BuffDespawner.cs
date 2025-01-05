using UnityEditor.VersionControl;
using UnityEngine;

public class BuffDespawner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            // Destroy the clock when the player collides with it
            Destroy(gameObject);
            Debug.Log("Object destroyed on trigger with player.");
        }
    }
}
