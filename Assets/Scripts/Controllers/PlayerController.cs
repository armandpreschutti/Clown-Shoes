using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerInput playerController;
    [SerializeField] InputAction moveInput;
    [SerializeField] InputAction jumpInput;
    [SerializeField] WheelJoint2D wheel;
    [SerializeField] float moveSpeed;
    [SerializeField] float maxTorque;

    private void OnEnable()
    {
        jumpInput.performed += Jump;
    }
    private void OnDisable()
    {
        jumpInput.performed -= Jump;
    }

    private void Awake()
    {
        moveInput = playerController.actions["Move"];
        jumpInput = playerController.actions["Jump"];
    }
    private void FixedUpdate()
    {
        PlayerMovement();      
    }

    public void PlayerMovement()
    {
        if (Mathf.Abs(moveInput.ReadValue<float>()) > 0.1f)
        {
            // Set motor speed based on input
            JointMotor2D motor = wheel.motor;
            motor.motorSpeed = moveSpeed * moveInput.ReadValue<float>();
            wheel.motor = motor;
            wheel.useMotor = true;
        }
        else
        {
            wheel.useMotor = false;
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Player Has Jumped");
    }
}
