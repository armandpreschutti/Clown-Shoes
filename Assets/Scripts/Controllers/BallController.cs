using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    [SerializeField] PlayerInput playerController;

    [SerializeField] InputAction moveInput;
    [SerializeField] WheelJoint2D ball;
    [SerializeField] float moveSpeed;
    [SerializeField] float maxTorque;
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] bool isDriving;

    void FixedUpdate()
    {
        PlayerMovement();
    }

    public void PlayerMovement()
    {
        if(isDriving)
        {
            if (Mathf.Abs(moveInput.ReadValue<float>()) > 0.1f && isDriving)
            {
                JointMotor2D motor = ball.motor;
                motor.motorSpeed = moveSpeed * moveInput.ReadValue<float>();
                ball.motor = motor;
                ball.useMotor = true;
            }
            else
            {
                ball.useMotor = false;
            }
        }
        else
        {
            return;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player landed on ball");
            collision.GetComponent<PlayerController>().AttachBall(ball);
            ball.connectedBody = collision.GetComponent<Rigidbody2D>();
            playerController = collision.GetComponent<PlayerInput>();
            moveInput = playerController.actions["Move"];
            isDriving = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player jumped off ball");
            collision.GetComponent<PlayerController>().DetachBall();
            playerController = null;
            isDriving = false;
        }
    }
}
