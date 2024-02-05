using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerInput playerController;
    [SerializeField] InputAction moveInput;
    [SerializeField] InputAction jumpInput;
    [SerializeField] InputAction abortInput;
    [SerializeField] InputAction mainMenuInput;
    [SerializeField] InputAction restartInput;
    [SerializeField] WheelJoint2D connectedBall;
    
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] Collider2D playerCol;
    [SerializeField] float jumpForce;
    [SerializeField] float airControl;
    [SerializeField] Animator anim;
    [SerializeField] float respawnHeight;
    public GameObject lastBall;
    public bool onBall;
    public bool alive = true;
    public static Action<PlayerController> OnRespawn;
    public static Action OnJump;
    public static Action OnGround;
    public static Action OnLaunch;
    


    private void OnEnable()
    {
        jumpInput.performed += Jump;
        abortInput.performed += Abort;
        restartInput.performed += RestartLevel;
        mainMenuInput.performed += ReturnToMenu;
        BalanceController.OnFall += LaunchPlayer;
        FinishLineHandler.OnFinish += DestoryPlayer;
    }
    private void OnDisable()
    {
        jumpInput.performed -= Jump;
        abortInput.performed -= Abort;
        restartInput.performed -= RestartLevel;
        mainMenuInput.performed -= ReturnToMenu;
        BalanceController.OnFall -= LaunchPlayer;
        FinishLineHandler.OnFinish -= DestoryPlayer;
    }

    private void Awake()
    {
        moveInput = playerController.actions["Move"];
        jumpInput = playerController.actions["Jump"];
        abortInput = playerController.actions["Abort"];
        restartInput = playerController.actions["RestartLevel"];
        mainMenuInput = playerController.actions["MainMenu"];
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
            OnJump?.Invoke();
            connectedBall.connectedBody = null;
            playerRb.AddForce(new Vector2(moveInput.ReadValue<float>(), 1f) * jumpForce);
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

    public void RestartLevel(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("ClownCollegeLayout");
    }

    public void ReturnToMenu(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("MainMenu");
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
        if (connectedBall != null)
        {
            connectedBall.useMotor = false;
            connectedBall.connectedBody = null;
        }
        OnLaunch?.Invoke();
        playerCol.enabled = false;
        playerRb.AddForce(new Vector2(Random.Range(-2f, 2f), Random.Range(1f, 2f) * jumpForce));
        playerRb.freezeRotation = false;
        playerRb.AddTorque(Random.Range(-1f, 1f) * 1000);
        /*Vector2 detachForce = (new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized * 120f);
        float detachAngularForce = Random.Range(-1, 1) * 360;
        playerRb.AddForce(detachForce);*/

        //playerRb.angularVelocity = detachAngularForce;
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
        if (collision.gameObject.CompareTag("Ground") && !onBall)
        {
            OnGround?.Invoke();
            LaunchPlayer();
        }
    }

    public void DestoryPlayer()
    {
        //Destroy(this.gameObject);
        this.gameObject.SetActive(false);
    }
}
