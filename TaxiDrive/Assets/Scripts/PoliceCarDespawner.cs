
using UnityEngine;
using UnityEngine.EventSystems;

public class PoliceCarDespawner : MonoBehaviour
{
    public VehicleController vehicleController;
    public float timeThreshold = 5f;
    public float distanceThreshold = 300f;

    private float speed;
    private float timeAtZeroSpeed;
    private Transform player;

    void Update()
    {
        speed = vehicleController.GetSpeed();

        if (Mathf.Abs(speed) < 0.1f)
        {
            timeAtZeroSpeed += Time.deltaTime;
        }
        else
        {
            timeAtZeroSpeed = 0f;
        }

        if (timeAtZeroSpeed >= timeThreshold)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer > distanceThreshold)
            {
                DespawnCar();
            }
        }
    }

    void DespawnCar()
    {
        Debug.Log($"Police car despawned.");
        Destroy(gameObject);
    }

    public void SetPlayerTransform(Transform playerTransform)
    {
        player = playerTransform;
    }
}