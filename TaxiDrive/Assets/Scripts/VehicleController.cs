using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VehicleController : MonoBehaviour
{
    public float motorPower;
    public float brakePower;
    public AnimationCurve steeringCurve;

    private float speed;
    private float slipAngle;
    private float gasInput;
    private float steeringInput;
    private float brakeInput;

    private Rigidbody rb;
    public WheelColliders wheelColliders;
    public WheelMeshes wheelMeshes;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        speed = rb.velocity.magnitude;
        ApplyMotor();
        ApplySteering();
        ApplyBrake();
        ApplyWheelPositions();
    }

    public void SetInputs(float gasInput, float steeringInput)
    {
        this.gasInput = gasInput;
        this.steeringInput = steeringInput;

        slipAngle = Vector3.Angle(transform.forward, rb.velocity - transform.forward);

        //fixed code to brake even after going on reverse by Andrew Alex 
        float movingDirection = Vector3.Dot(transform.forward, rb.velocity);
        if (movingDirection < -0.5f && gasInput > 0)
        {
            brakeInput = Mathf.Abs(gasInput);
        }
        else if (movingDirection > 0.5f && gasInput < 0)
        {
            brakeInput = Mathf.Abs(gasInput);
        }
        else
        {
            brakeInput = 0;
        }
    }

    void ApplyBrake()
    {
        wheelColliders.wheelFR.brakeTorque = brakeInput * brakePower * 0.7f;
        wheelColliders.wheelFL.brakeTorque = brakeInput * brakePower * 0.7f;

        wheelColliders.wheelRR.brakeTorque = brakeInput * brakePower * 0.3f;
        wheelColliders.wheelRL.brakeTorque = brakeInput * brakePower * 0.3f;
    }
    void ApplyMotor()
    {
        wheelColliders.wheelRR.motorTorque = motorPower * gasInput;
        wheelColliders.wheelRL.motorTorque = motorPower * gasInput;
    }

    void ApplySteering()
    {
        float steeringAngle = steeringInput * steeringCurve.Evaluate(speed);
        if (slipAngle < 120f)
        {
            steeringAngle += Vector3.SignedAngle(transform.forward, rb.velocity + transform.forward, Vector3.up);
        }
        steeringAngle = Mathf.Clamp(steeringAngle, -90f, 90f);
        wheelColliders.wheelFR.steerAngle = steeringAngle;
        wheelColliders.wheelFL.steerAngle = steeringAngle;
    }

    void ApplyWheelPositions()
    {
        UpdateWheel(wheelColliders.wheelFR, wheelMeshes.wheelFR);
        UpdateWheel(wheelColliders.wheelFL, wheelMeshes.wheelFL);
        UpdateWheel(wheelColliders.wheelRR, wheelMeshes.wheelRR);
        UpdateWheel(wheelColliders.wheelRL, wheelMeshes.wheelRL);
    }

    void UpdateWheel(WheelCollider coll, MeshRenderer wheelMesh)
    {
        Quaternion quat;
        Vector3 position;
        coll.GetWorldPose(out position, out quat);
        wheelMesh.transform.position = position;
        wheelMesh.transform.rotation = quat;
    }
}

[System.Serializable]
public class WheelColliders
{
    public WheelCollider wheelFR;
    public WheelCollider wheelFL;
    public WheelCollider wheelRR;
    public WheelCollider wheelRL;
}

[System.Serializable]
public class WheelMeshes
{
    public MeshRenderer wheelFR;
    public MeshRenderer wheelFL;
    public MeshRenderer wheelRR;
    public MeshRenderer wheelRL;
}