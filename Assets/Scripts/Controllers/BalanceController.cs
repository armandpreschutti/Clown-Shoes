using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BalanceController : MonoBehaviour
{
    public PlayerInput playerInput;
    public InputAction moveInput;
    public float currentBalance;
    public bool leanDirection;
    public float moveSpeed = 0.1f;
    public int lastInputDirection = 0;
    public  float value;
    private void Awake()
    {
        moveInput = playerInput.actions["Move"];
    }

    private void Update()
    {
        currentBalance = moveInput.ReadValue<float>();
        HandleBalance();
    }

    public void HandleBalance()
    {
        value += lastInputDirection * moveSpeed * Time.deltaTime;

        if (currentBalance > 0.1f)
        {
            lastInputDirection = 1;
        }
        else if(currentBalance < -.01f)
        {
            lastInputDirection = -1;
        }      
    } 
}

