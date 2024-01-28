using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerInput playerController;
    [SerializeField] InputAction moveInput;
    [SerializeField] InputAction jumpInput;
    [SerializeField] WheelJoint2D connectedBall;
    [SerializeField] GameObject lastBall;
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] Collider2D playerCol;
    [SerializeField] float jumpForce;
    [SerializeField] float airControl;
    [SerializeField] BalanceController balanceController;
    public bool onBall;
    public bool alive = true;

    private void OnEnable()
    {
        jumpInput.performed += Jump;
        BalanceController.OnFall += LaunchPlayer;
    }
    private void OnDisable()
    {
        jumpInput.performed -= Jump;
        BalanceController.OnFall -= LaunchPlayer;
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
        if(connectedBall == null && alive)
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
        if (/*!isGrounded && */connectedBall != null && alive)
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
        if (connectedBall == null && alive)
        {
            connectedBall = wheel;
            lastBall = wheel.gameObject;
            onBall = true;
        }
        else
        {
            return;
        }

    }

    public void DetachBall()
    {
        connectedBall = null;
        onBall = false;
    }


    public Boolean IsAttachedToBall() {
        return connectedBall != null;
    }

    public void LaunchPlayer()
    {
        Debug.Log("Player was launched");
        connectedBall.useMotor = false;
        connectedBall.connectedBody = null;
        playerCol.enabled = false;
        playerRb.AddForce(Vector2.up * jumpForce *4);
        Vector2 detachForce = (new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized * 120f);
        float detachAngularForce = Random.Range(-1, 1) * 3600;
        playerRb.AddForce(detachForce);
        playerRb.freezeRotation = false;
        playerRb.angularVelocity = detachAngularForce;
        GetComponent<CapsuleCollider2D>().enabled = false; 
        //balanceController.enabled=false
        alive = false;
        StartCoroutine(RespawnDelay());
    }


    public IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(3f);
        RespawnPlayer();
    }
    public void RespawnPlayer()
    {
        connectedBall = lastBall.GetComponent<WheelJoint2D>();
        connectedBall.connectedBody = playerRb;
        connectedBall.useMotor = true;
        playerCol.enabled = true;

        playerRb.freezeRotation = true;
        playerRb.angularVelocity = 0;
        alive = true;
        //balanceController.enabled=true
    }

}
