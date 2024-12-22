using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private VehicleController vehicleController;

    [System.Serializable]
    public class WheelEffectsObjects
    {
        public GameObject wheelFR;
        public GameObject wheelFL;
        public GameObject wheelRR;
        public GameObject wheelRL;
    }

    public WheelEffectsObjects wheelEffectsObjects;



    void Start()
    {
        vehicleController = GetComponent<VehicleController>();
    }

    void Update()
    {
        float gasInput = Input.GetAxis("Vertical");
        float steeringInput = Input.GetAxis("Horizontal");

        // Pass inputs to the vehicle controller
        vehicleController.SetInputs(gasInput, steeringInput);

        // Handle wheel effects
        HandleWheelEffects();
    }

    void HandleWheelEffects()
    {
        List<GameObject> wheels = new List<GameObject> {
            wheelEffectsObjects.wheelFR,
            wheelEffectsObjects.wheelFL,
            wheelEffectsObjects.wheelRR,
            wheelEffectsObjects.wheelRL
        };

        foreach (var wheel in wheels)
        {
            TrailRenderer trail = wheel.GetComponentInChildren<TrailRenderer>();
            ParticleSystem smoke = wheel.GetComponentInChildren<ParticleSystem>();
            if (Input.GetKey(KeyCode.Space))
            {
                trail.emitting = true;
                smoke.Emit(1);
            }
            else
            {
                trail.emitting = false;
            }

        }
    }
}
