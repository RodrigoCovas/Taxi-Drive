using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Speedometer : MonoBehaviour
{
    public Rigidbody carRigidbody;
    public TMP_Text speedText;

    void Start()
    {
        speedText.text = $"0 km/h";
    }

    void Update()
    {
        float speed = carRigidbody.velocity.magnitude * 3.6f; // Convert to km/h
        speedText.text = $"{speed:F0} km/h";
    }
}
