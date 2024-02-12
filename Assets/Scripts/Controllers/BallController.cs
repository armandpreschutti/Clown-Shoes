using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class BallController : MonoBehaviour
{
    [SerializeField] PlayerInput playerController;
    [SerializeField] InputAction moveInput;
    [SerializeField] WheelJoint2D ball;
    [SerializeField] float moveSpeed;
    [SerializeField] bool isDriving;
    private float lastInputDirection;

    public Vector3 originPoint;

    private void OnEnable()
    {
        PlayerController.OnRespawn += RepositionBall;
    }

    private void OnDisable()
    {
        PlayerController.OnRespawn -= RepositionBall;
    }

    private void Start()
    {
        originPoint = transform.position;
    }

    void FixedUpdate()
    {
        PlayerMovement();
    }

    public void PlayerMovement()
    {
        if(isDriving)
        {
            /* GetMoveDirection();
             JointMotor2D motor = ball.motor;
             motor.motorSpeed = moveSpeed * lastInputDirection;
             ball.motor = motor;
             ball.useMotor = true;*/
            if (Mathf.Abs(moveInput.ReadValue<float>()) > 0.1f)
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
            //ball.useMotor = false;
            return;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && ball.connectedBody == null)
        {
            Debug.Log("Player landed on ball");
            GameObject player = collision.gameObject;
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null && !playerController.IsAttachedToBall()) {
                player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                playerController.AttachBall(ball);
                ball.connectedBody = collision.GetComponent<Rigidbody2D>();
                PlayerInput input = collision.GetComponent<PlayerInput>();
                moveInput = input.actions["Move"];
            }
            isDriving = true;
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
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
    
    public void RepositionBall(PlayerController playerController)
    {
        transform.position = originPoint;
        transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0f;
    }
    public void GetMoveDirection()
    {
        if (moveInput.ReadValue<float>() > 0.1f)
        {
            lastInputDirection = 1;

        }
        else if (moveInput.ReadValue<float>() < -.01f)
        {
            lastInputDirection = -1;
        }
    }
}
