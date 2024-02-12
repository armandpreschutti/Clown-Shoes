using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BalanceController : MonoBehaviour
{
    public PlayerInput playerInput;
    public PlayerController playerController;
    public InputAction moveInput;
    public float input;
    public float balanceSpeed;
    public int lastInputDirection = 0;
    public  float currentBalance;
    public Slider balanceIndicator;
    public GameObject balanceBar;
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] float velocityBuffer;
    public float balanceRate;


    public static Action OnFall;

    private void Awake()
    {
        moveInput = playerInput.actions["Move"];
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        PlayerController.OnRespawn += ResetBalance;
        PlayerController.OnDeath += DeactivateBalance;
        PlayerController.OnJump += DeactivateBalance;
        PlayerController.OnLand += ReactivateBalance;
        
    }
    private void OnDisable()
    {
        PlayerController.OnRespawn -= ResetBalance;
        PlayerController.OnDeath -= DeactivateBalance;
        PlayerController.OnJump -= DeactivateBalance;
        PlayerController.OnLand -= ReactivateBalance;

    }

    private void Update()
    {
        if (playerController.onBall)
        {
            input = moveInput.ReadValue<float>();
            BalanceInput();
            Falling();
        }
        else
        {
            return;
        }
       
    }

    public void BalanceInput()
    {
        balanceIndicator.value = currentBalance;
       /* balanceRate = ((lastInputDirection * balanceSpeed * (MathF.Abs(playerRb.velocity.x) / velocityBuffer))) * Time.deltaTime;
        currentBalance += balanceRate;

        if (input > 0.1f)
        {
            lastInputDirection = 1;

        }
        else if (input < -.01f)
        {
            lastInputDirection = -1;
        }*/
        if (input != 0 || playerRb.velocity.x > 6f)
        {
            balanceRate = ((lastInputDirection * balanceSpeed * (MathF.Abs(playerRb.velocity.x) / velocityBuffer))) * Time.deltaTime;
            currentBalance += balanceRate;

            if (input > 0.1f)
            {
                lastInputDirection = 1;
            }
            else if (input < -.01f)
            {
                lastInputDirection = -1;
            }
        }
        else
        {
            if(currentBalance != 0)
            {
                balanceRate = ((lastInputDirection * balanceSpeed)) * Time.deltaTime;
                currentBalance += balanceRate;
            }
            else
            {
                return;
            }

        }

    } 
    public void Falling()
    {
        if(Mathf.Abs(currentBalance) > 3)
        {
            Debug.Log("Player had fallen");            
            OnFall?.Invoke();
            currentBalance = 0;
        }
    }

    public void DeactivateBalance()
    {
        balanceBar.SetActive(false);
    }
    public void ReactivateBalance()
    {
        balanceBar.SetActive(true);
    }
    public void ResetBalance(PlayerController playerController)
    {
        currentBalance = 0;
        ReactivateBalance();
    }

}

