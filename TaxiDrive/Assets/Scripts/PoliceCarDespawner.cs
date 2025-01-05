
using UnityEngine;

public class PoliceCarDespawner : MonoBehaviour
{
    public VehicleController vehicleController;
    private float speed;
    private float timeAtZeroSpeed;
    public float timeThreshold = 5f;

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
            DespawnCar();
        }
    }

    void DespawnCar()
    {
        Destroy(gameObject);
    }
}