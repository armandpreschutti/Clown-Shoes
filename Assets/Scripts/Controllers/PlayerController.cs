using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerInput playerController;
    [SerializeField] InputAction moveInput;
    [SerializeField] InputAction jumpInput;
    [SerializeField] InputAction abortInput;

    [SerializeField] WheelJoint2D connectedBall;
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] Collider2D playerCol;

    [SerializeField] float respawnHeight;
    public GameObject lastBall;
    public bool onBall;
    public bool alive = true;

    [SerializeField] float jumpForce;
    [SerializeField] float jumpAngle;
    [SerializeField] bool canDoubleJump;
    [SerializeField] float airControl;
    [SerializeField] float airControlMaxThreshold;
    [SerializeField] float airControlMinThreshold;

    public static Action<PlayerController> OnRespawn;
    public static Action OnJump;
    public static Action OnDoubleJump;
    public static Action OnGround;
    public static Action OnDeath;
    public static Action OnLand;


    private void OnEnable()
    {
        jumpInput.performed += Jump;
        abortInput.performed += Abort;
        BalanceController.OnFall += LaunchPlayer;
        FinishLineHandler.OnFinish += DestoryPlayer;
        PauseController.OnPause += TogglePauseState;
    }
    private void OnDisable()
    {
        jumpInput.performed -= Jump;
        abortInput.performed -= Abort;
        BalanceController.OnFall -= LaunchPlayer;
        FinishLineHandler.OnFinish -= DestoryPlayer;
        PauseController.OnPause -= TogglePauseState;
    }

    private void Awake()
    {
        moveInput = playerController.actions["Move"];
        jumpInput = playerController.actions["Jump"];
        abortInput = playerController.actions["Abort"];
    }

    private void FixedUpdate()
    {
        AirControl();      
    }

    public void AirControl()
    {
        if(alive && !onBall && playerRb.velocity.y < airControlMaxThreshold && Mathf.Abs(moveInput.ReadValue<float>()) > .1f)
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

        if (alive)
        {
            if (connectedBall != null)
            {
                OnJump?.Invoke();
                connectedBall.connectedBody = null;
                playerRb.AddForce(new Vector2(0f, jumpAngle) * jumpForce, ForceMode2D.Impulse);
                canDoubleJump = true;
            }

            else if (connectedBall == null && canDoubleJump)
            {
                OnDoubleJump?.Invoke();
                canDoubleJump = false;
                playerRb.velocity= Vector2.zero;
                playerRb.AddForce(new Vector2(0f, jumpAngle) * jumpForce, ForceMode2D.Impulse);
                canDoubleJump = false;
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

    public void Abort(InputAction.CallbackContext context)
    {
        LaunchPlayer();
    }

    public void AttachBall(WheelJoint2D wheel)
    {
        if (connectedBall == null && alive)
        {
            OnLand?.Invoke();
            playerRb.velocity = Vector2.zero;
            connectedBall = wheel;
            lastBall = wheel.gameObject;
            onBall = true;
            canDoubleJump = false;
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
        if (connectedBall != null)
        {
            connectedBall.useMotor = false;
            connectedBall.connectedBody = null;
        }
        OnDeath?.Invoke();
        playerCol.enabled = false;
        playerRb.AddForce(new Vector2(Random.Range(-2f, 2f), Random.Range(1f, 2f) * jumpForce));
        playerRb.freezeRotation = false;
        playerRb.AddTorque(Random.Range(-1f, 1f) * 1000);
        GetComponent<CapsuleCollider2D>().enabled = false; 
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
        OnRespawn?.Invoke(this);
        transform.eulerAngles = Vector3.zero;
        playerCol.enabled = true;
        playerRb.freezeRotation = true;
        playerRb.angularVelocity = 0;
        playerRb.velocity = Vector2.zero;
        alive = true;
        transform.position = lastBall.transform.position + new Vector3 (0, respawnHeight,0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            OnGround?.Invoke();
            LaunchPlayer();
        }
    }

    public void DestoryPlayer()
    {
        this.gameObject.SetActive(false);
    }
    public void TogglePauseState(bool value)
    {
        if(value)
        {
            moveInput.Disable();
            jumpInput.Disable();
            abortInput.Disable();
        }
        else
        {
            Debug.Log("Controls not paused");
            moveInput.Enable();
            jumpInput.Enable();
            abortInput.Enable();
        }
       
    }
}
