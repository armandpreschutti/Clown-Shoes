using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerInput playerController;
    [SerializeField] InputAction moveInput;
    [SerializeField] InputAction jumpInput;
    [SerializeField] WheelJoint2D connectedBall;
    
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] Collider2D playerCol;
    [SerializeField] float jumpForce;
    [SerializeField] float airControl;
    [SerializeField] Animator anim;

    public GameObject lastBall;
    public bool onBall;
    public bool alive = true;
    public static Action<PlayerController> OnRespawn;
    public CinemachineVirtualCamera vCam;

    private void OnEnable()
    {
        jumpInput.performed += Jump;
        BalanceController.OnFall += LaunchPlayer;
        FinishLineHandler.OnFinish += DestoryPlayer;
    }
    private void OnDisable()
    {
        jumpInput.performed -= Jump;
        BalanceController.OnFall -= LaunchPlayer;
        FinishLineHandler.OnFinish -= DestoryPlayer;
    }

    private void Awake()
    {
        moveInput = playerController.actions["Move"];
        jumpInput = playerController.actions["Jump"];
        vCam = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
    }

    private void FixedUpdate()
    {
        PlayerMovement();      
    }

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
        if (connectedBall != null && alive)
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
            anim.SetBool("Jump", false);
        }
        else
        {
            return;
        }

    }

    public void DetachBall()
    {
        anim.SetBool("Jump", true );
        connectedBall = null;
        onBall = false;        
    }


    public Boolean IsAttachedToBall() {
        return connectedBall != null;
    }

    public void LaunchPlayer()
    {
        vCam.enabled = false;
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
        vCam.enabled = true;
        OnRespawn?.Invoke(this);
        transform.eulerAngles = Vector3.zero;
        playerCol.enabled = true;
        playerRb.freezeRotation = true;
        playerRb.angularVelocity = 0;
        alive = true;
        transform.position = lastBall.transform.position + new Vector3 (0, 10,0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            vCam.enabled = false;
            playerCol.enabled = false;
            playerRb.AddForce(Vector2.up * jumpForce * 4);
            Vector2 detachForce = (new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized * 120f);
            float detachAngularForce = Random.Range(-1, 1) * 3600;
            playerRb.AddForce(detachForce);
            playerRb.freezeRotation = false;
            playerRb.angularVelocity = detachAngularForce;
            GetComponent<CapsuleCollider2D>().enabled = false;
            alive = false;
            StartCoroutine(RespawnDelay());
        }
    }

    public void DestoryPlayer()
    {
        Destroy(this.gameObject);
    }
}
