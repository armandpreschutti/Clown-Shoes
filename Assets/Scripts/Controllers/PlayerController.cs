using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerInput playerController;
    [SerializeField] InputAction moveInput;
    [SerializeField] InputAction jumpInput;
    [SerializeField] WheelJoint2D connectedBall;
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] float jumpForce;
    [SerializeField] float airControl;
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
        if(connectedBall == null)
        {
            playerRb.velocity = new Vector2(moveInput.ReadValue<float>() * airControl, playerRb.velocity.y);
        }
        else
        {
            return;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Player Has Jumped");
        connectedBall.connectedBody = null;
        playerRb.AddForce(Vector2.up * jumpForce);

    }

    public void AttachBall(WheelJoint2D wheel)
    {
        connectedBall = wheel;
    }
    public void DetachBall()
    {
        connectedBall = null;
    }
}
