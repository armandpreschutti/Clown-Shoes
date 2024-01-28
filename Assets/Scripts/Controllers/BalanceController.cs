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
    public float currentBalance;
    public bool leanDirection;
    public float moveSpeed = 0.1f;
    public int lastInputDirection = 0;
    public  float value;
    public Slider balanceIndicator;
    public static Action OnFall;

    private void Awake()
    {
        moveInput = playerInput.actions["Move"];
    }

    private void Update()
    {
        if (playerController.onBall)
        {
            currentBalance = moveInput.ReadValue<float>();
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
        value += lastInputDirection * moveSpeed * Time.deltaTime;
        balanceIndicator.value = value;

        if (currentBalance > 0.1f)
        {
            lastInputDirection = 1;
        }
        else if(currentBalance < -.01f)
        {
            lastInputDirection = -1;
        }      
    } 
    public void Falling()
    {
        if(Mathf.Abs(value) > 3)
        {
            Debug.Log("Player had fallen");            
            OnFall?.Invoke();
            value = 0;
        }
    }
}

