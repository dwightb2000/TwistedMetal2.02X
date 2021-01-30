using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Axel
{
    Front,
    Rear
}


[Serializable]
public struct Wheel
{
    public GameObject model;
    public WheelCollider collider;
    public Axel axel;
}

public class CarController : MonoBehaviour
{
    [SerializeField]
    private float maxAcceleration = 20.0f;
    [SerializeField]
    private float turnSensitivity = 1.0f;
    [SerializeField]
    private float maxSteerAngle = 45.0f;
    [SerializeField]
    private List<Wheel> wheels;

    private float inputX, inputY;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        GetInputs();
    }

    private void GetInputs() 
    { 
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
    }

// FixedUpdate is best for physics
private void FixedUpdate()
    {
        Move();
        Turn();
    }

private void Move()
    {
        foreach (var wheel in wheels)
        {
            wheel.collider.motorTorque = inputY* maxAcceleration * 500 * Time.deltaTime;
        }
    }

private void Turn()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                var steerAngle = inputX * turnSensitivity * maxSteerAngle;
                wheel.collider.steerAngle = Mathf.Lerp(wheel.collider.steerAngle, steerAngle, 0.5f);
            }
        }
    }

}
