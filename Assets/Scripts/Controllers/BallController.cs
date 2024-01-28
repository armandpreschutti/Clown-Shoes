using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    [SerializeField] PlayerInput playerController;
    [SerializeField] Transform attachmentPoint;
    [SerializeField] InputAction moveInput;
    [SerializeField] WheelJoint2D ball;
    [SerializeField] float moveSpeed;
    [SerializeField] float maxTorque;
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] bool isDriving;

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
            GameObject player = collision.gameObject;
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null && !playerController.IsAttachedToBall()) {
                player.transform.position = attachmentPoint.position;
                player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                playerController.AttachBall(ball);
                ball.connectedBody = collision.GetComponent<Rigidbody2D>();
                PlayerInput input = collision.GetComponent<PlayerInput>();
                moveInput = input.actions["Move"];
            }
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
    
    public void RepositionBall(PlayerController playerController)
    {
        if(playerController.lastBall == this.gameObject)
        {
            transform.position = originPoint;
            transform.rotation = Quaternion.identity;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0f;
        }
        else
        {
            return;
        }
       
        
    }
}
