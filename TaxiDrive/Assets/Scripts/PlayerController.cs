using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private VehicleController vehicleController;

    void Start()
    {
        vehicleController = GetComponent<VehicleController>();
    }

    void Update()
    {
        float gasInput = Input.GetAxis("Vertical");
        float steeringInput = Input.GetAxis("Horizontal");
        vehicleController.SetInputs(gasInput, steeringInput);
    }
}