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
    [SerializeField] GameObject lastBall;
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] float jumpForce;
    [SerializeField] float airControl;
    [SerializeField] bool isGrounded;

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

    // Player Control Logic

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
        if (/*!isGrounded && */connectedBall != null)
        {
            connectedBall.connectedBody = null;
            playerRb.AddForce(Vector2.up * jumpForce);
        }
        else
        {
            return;
        }

    }

    // Ball Attachment Logic

    public void AttachBall(WheelJoint2D wheel)
    {
        if (connectedBall == null)
        {
            connectedBall = wheel;
            lastBall = wheel.gameObject;
        }
        else
        {
            return;
        }

    }

    public void DetachBall()
    {
        connectedBall = null;
    }


    public Boolean IsAttachedToBall() {
        return connectedBall != null;
    }

    // Trigger Box Logic

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Player is grounded");
            isGrounded = true;
        }
        else
        {
            return;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Player is no longer grounded");
            isGrounded = false;
        }
        else
        {
            return;
        }
    }*/
}
