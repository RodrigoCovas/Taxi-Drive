using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 1.0f;
    public float wheelRotationSpeed = 300.0f;
    public float maxSteeringAngle = 30.0f;

    public Transform Wheel_FL;
    public Transform Wheel_FR;
    public Transform Wheel_RL;
    public Transform Wheel_RR;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 movement)
    {
        rb.AddForce(movement * speed);
        Rotate(movement);
        RotateWheels(movement);
        SteerFrontWheels();
    }

    public void Rotate(Vector3 movement)
    {
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
    private void RotateWheels(Vector3 direction)
    {
        float rotationAmount = wheelRotationSpeed * Time.deltaTime * direction.magnitude;

        if (Wheel_FL != null)
        {
            Wheel_FL.Rotate(Vector3.right, rotationAmount);
        }
        if (Wheel_FR != null)
        {
            Wheel_FR.Rotate(Vector3.right, rotationAmount);
        }
        if (Wheel_RL != null)
        {
            Wheel_RL.Rotate(Vector3.right, rotationAmount);
        }
        if (Wheel_RR != null)
        {
            Wheel_RR.Rotate(Vector3.right, rotationAmount);
        }

    }
    private void SteerFrontWheels()
    {
        float steerInput = Input.GetAxis("Horizontal");

        float steeringAngle = maxSteeringAngle * steerInput;

        // Rotate the front wheels left and right
        if (Wheel_FL != null)
        {
            Wheel_FL.localRotation = Quaternion.Euler(Wheel_FL.localRotation.eulerAngles.x, steeringAngle, 0);
        }

        if (Wheel_FR != null)
        {
            Wheel_FR.localRotation = Quaternion.Euler(Wheel_FR.localRotation.eulerAngles.x, steeringAngle, 0);
        }
    }
}
