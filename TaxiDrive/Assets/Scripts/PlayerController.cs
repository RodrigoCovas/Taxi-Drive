using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VehicleController))]
public class PlayerController : MonoBehaviour
{
    private VehicleController vehicleController;

    void Start()
    {
        vehicleController = GetComponent<VehicleController>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(moveHorizontal, 0.0f, moveVertical);

        vehicleController.Move(direction);
    }
}
