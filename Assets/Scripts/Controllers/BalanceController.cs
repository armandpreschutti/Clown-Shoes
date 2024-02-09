using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BalanceController : MonoBehaviour
{
    public PlayerInput playerInput;
    public PlayerController playerController;
    public InputAction moveInput;
    public float input;
    public bool leanDirection;
    public float moveSpeed;
    public int lastInputDirection = 0;
    public  float currentBalance;
    public Slider balanceIndicator;
    public SpriteRenderer spriteRenderer;
    public GameObject balanceBar;

    public static Action OnFall;

    private void Awake()
    {
        moveInput = playerInput.actions["Move"];
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
        currentBalance += lastInputDirection * moveSpeed * Time.deltaTime;
        balanceIndicator.value = currentBalance;

        if (input > 0.1f)
        {
            lastInputDirection = 1;
            spriteRenderer.flipX= false;

        }
        else if(input < -.01f)
        {
            lastInputDirection = -1;
            spriteRenderer.flipX = true;
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

