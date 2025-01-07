using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    private VehicleController vehicleController; // Reference to the vehicle controller
    public float detectionRange = 50f; // Maximum range to follow the player
    public float followDistance = 10f; // Distance to maintain from the player

    [SerializeField] private float maxSteeringAngle = 30f; // Max steering angle
    [SerializeField] private float maxSpeed = 20f; // Max speed in units per second
    [SerializeField] private float accelerationFactor = 1f; // Controls acceleration

    private Rigidbody rb;

    void Start()
    {
        vehicleController = GetComponent<VehicleController>();
        rb = vehicleController.GetComponent<Rigidbody>();
    }

    void Update()
    {
        CalculateInput();
    }

    void CalculateInput()
    {
        if (Vector3.Distance(transform.position, player.position) > detectionRange)
        {
            vehicleController.SetInputs(0, 0); // Stop if out of range
            return;
        }

        // Calculate direction to the player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Calculate desired velocity
        Vector3 desiredVelocity = directionToPlayer * maxSpeed;
        Vector3 currentVelocity = rb.velocity;

        // Calculate gas input based on velocity difference
        float velocityDifference = desiredVelocity.magnitude - currentVelocity.magnitude;
        float gasInput = Mathf.Clamp(velocityDifference * accelerationFactor, -1f, 1f);

        // Calculate steering input
        Vector3 localTarget = transform.InverseTransformPoint(player.position);
        float angleToTarget = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;
        float steeringInput = Mathf.Clamp(angleToTarget / maxSteeringAngle, -1f, 1f);

        // Stop or reduce gasInput when near the player
        if (distanceToPlayer < followDistance)
            gasInput = 0;

        // Set inputs to the vehicle controller
        vehicleController.SetInputs(gasInput, steeringInput);
    }

    public void SetPlayerTransform(Transform playerTransform)
    {
        player = playerTransform;
    }
}
