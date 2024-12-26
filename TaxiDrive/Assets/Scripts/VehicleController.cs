using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VehicleController : MonoBehaviour
{
    public float motorPower;
    public float brakePower;
    public AnimationCurve steeringCurve;
    public float turnSensitivity;
    public float maxDecelerationFactor;
    public float timeDecelerationFactor;
    public float driftFactor = 0.95f;
    public float normalTraction = 1f;
    public float driftTraction = 0.5f;


    private bool isDrifting = false;
    private float speed;
    private float slipAngle;
    private float gasInput;
    private float steeringInput;
    private float brakeInput;
    private float decelerationTimer;

    private Rigidbody rb;
    private CarLights carLights;
    public WheelColliders wheelColliders;
    public WheelMeshes wheelMeshes;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
        carLights = GetComponent<CarLights>();
    }

    void Update()
    {
        speed = rb.velocity.magnitude;

        ApplyMotor();
        ApplySteering();
        ApplyBrake();
        ApplyDrift();
        ApplyWheelPositions();
    }

    public void SetInputs(float gasInput, float steeringInput)
    {
        this.gasInput = gasInput;
        this.steeringInput = steeringInput;

        slipAngle = Vector3.Angle(transform.forward, rb.velocity - transform.forward);

        if (gasInput != 0)
        {
            decelerationTimer = 0;
        }

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
    public void SetDrift(bool drifting)
    {
        isDrifting = drifting;
    }

    void ApplyBrake()
    {
        if (gasInput == 0)
        {
            decelerationTimer += Time.deltaTime;
        } 

        float decelerationBrake = Mathf.Lerp(0, maxDecelerationFactor, decelerationTimer / timeDecelerationFactor);

        wheelColliders.wheelFR.brakeTorque = brakeInput * brakePower * 0.7f + decelerationBrake;
        wheelColliders.wheelFL.brakeTorque = brakeInput * brakePower * 0.7f + decelerationBrake;

        wheelColliders.wheelRR.brakeTorque = brakeInput * brakePower * 0.3f + decelerationBrake;
        wheelColliders.wheelRL.brakeTorque = brakeInput * brakePower * 0.3f + decelerationBrake;

        carLights.BackLightsOn = brakeInput > 0;
        carLights.OperateBackLights();
    }
    void ApplyMotor()
    {
        if (gasInput == 0)
        {
            wheelColliders.wheelRR.motorTorque = 0;
            wheelColliders.wheelRL.motorTorque = 0;
        }
        else
        {
            wheelColliders.wheelRR.motorTorque = motorPower * gasInput;
            wheelColliders.wheelRL.motorTorque = motorPower * gasInput;
        }
    }

    void ApplySteering()
    {
        float steeringAngle = steeringInput * steeringCurve.Evaluate(speed);
        if (slipAngle < 120f)
        {
            steeringAngle += Vector3.SignedAngle(transform.forward, rb.velocity + transform.forward, Vector3.up);
        }
        steeringAngle = Mathf.Clamp(steeringAngle, -90f, 90f);
        wheelColliders.wheelFR.steerAngle = steeringAngle * turnSensitivity;
        wheelColliders.wheelFL.steerAngle = steeringAngle * turnSensitivity;

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

    void ApplyDrift()
    {
        if (isDrifting)
        {
            // Reduce rear wheel traction
            wheelColliders.wheelRR.sidewaysFriction = CreateDriftFriction(driftTraction);
            wheelColliders.wheelRL.sidewaysFriction = CreateDriftFriction(driftTraction);

            // Add lateral force for slide (optional for more dramatic drift)
            Vector3 lateralForce = transform.right * rb.velocity.magnitude * driftFactor;
            rb.AddForce(lateralForce, ForceMode.Force);
        }
        else
        {
            // Reset to normal traction
            wheelColliders.wheelRR.sidewaysFriction = CreateDriftFriction(normalTraction);
            wheelColliders.wheelRL.sidewaysFriction = CreateDriftFriction(normalTraction);
        }
    }

    WheelFrictionCurve CreateDriftFriction(float traction)
    {
        WheelFrictionCurve frictionCurve = wheelColliders.wheelRR.sidewaysFriction;
        frictionCurve.stiffness = traction;
        return frictionCurve;
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
}