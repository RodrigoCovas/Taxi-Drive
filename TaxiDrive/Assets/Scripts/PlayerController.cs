using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private VehicleController vehicleController;
    private CarLights carLights;
    public SystemMessages messages;

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
    private bool controlsInverted = false;
    private float inversionDuration = 20f;
    private float currentInversionTime = 0f;

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
        GetInput();
    }

    void GetInput()
    {
        float gasInput = Input.GetAxis("Vertical");
        float steeringInput = Input.GetAxis("Horizontal");

        if (controlsInverted)
        {
            gasInput = -gasInput;
            steeringInput = -steeringInput;

            currentInversionTime -= Time.deltaTime;
            if (currentInversionTime <= 0f)
            {
                controlsInverted = false;
                Debug.Log("Control inversion ended");
            }
        }

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mirror"))
        {
            Debug.Log("Mirror object triggered, Controls are inverted");
            controlsInverted = true;
            currentInversionTime = inversionDuration;
            string message = "You found a MIRROR" + "\n"
                            + "Your controlls are inverted for " + inversionDuration + " seconds";
            messages.WriteMessage(message, 2.5f);
        }
    }
}
