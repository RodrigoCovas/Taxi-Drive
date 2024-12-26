using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private VehicleController vehicleController;
    private CarLights carLights;

    [System.Serializable]
    public class WheelEffectsObjects
    {
        public GameObject wheelFR;
        public GameObject wheelFL;
        public GameObject wheelRR;
        public GameObject wheelRL;
    }

    public WheelEffectsObjects wheelEffectsObjects;

    private bool isDrifting;

    void Start()
    {
        vehicleController = GetComponent<VehicleController>();
        carLights = GetComponent<CarLights>();

        if (carLights != null)
        {
            carLights.isPlayer = true;
        }
    }

    void Update()
    {
        float gasInput = Input.GetAxis("Vertical");
        float steeringInput = Input.GetAxis("Horizontal");

        isDrifting = Input.GetKey(KeyCode.Space);

        vehicleController.SetInputs(gasInput, steeringInput);
        vehicleController.SetDrift(isDrifting);

        HandleWheelEffects();

        if (Input.GetKeyDown(KeyCode.L))
        {
            carLights.OperateFrontLights();
        }
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
            if (isDrifting)
            {
                if (trail != null) trail.emitting = true;
                if (smoke != null) smoke.Emit(1);
            }
            else
            {
                if (trail != null) trail.emitting = false;
            }
        }
    }
}
