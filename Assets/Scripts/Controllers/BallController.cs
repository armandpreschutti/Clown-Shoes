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
    public WheelJoint2D ball;
    public float moveSpeed;
    public bool isDriving;

    public Vector3 originPoint;
    public Transform attatchmentPoint;

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
        attatchmentPoint.position = transform.position + new Vector3(0, 2, 0f);
        if(isDriving)
        {
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
                player.transform.position = attatchmentPoint.position;
                ball.connectedBody = collision.GetComponent<Rigidbody2D>();
                PlayerInput input = collision.GetComponent<PlayerInput>();
                moveInput = input.actions["Move"];
                isDriving = true;
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
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
}
