using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimationHandler : MonoBehaviour
{
    public PlayerInput playerInput;
    public InputAction moveInput;
    public SpriteRenderer spriteRenderer;
    public Animator anim;

    private void Awake()
    {
        playerInput= GetComponent<PlayerInput>();
        moveInput = playerInput.actions["Move"];
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PlayerController.OnJump += PlayJumpAnimation;
        PlayerController.OnDoubleJump+= PlayDoubleJumpAnimation;
        PlayerController.OnLand += PlayBalanceAnimation;
        PlayerController.OnRespawn += ResetAnimation;
        PlayerController.OnDeath += PlayDeathAnimation;
    }

    private void OnDisable()
    {
        PlayerController.OnJump -= PlayJumpAnimation;
        PlayerController.OnDoubleJump -= PlayDoubleJumpAnimation;
        PlayerController.OnLand -= PlayBalanceAnimation;
        PlayerController.OnRespawn -= ResetAnimation;
        PlayerController.OnDeath -= PlayDeathAnimation;
    }

    private void Update()
    {
        if (GetComponent<Rigidbody2D>().velocity.x > 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if (GetComponent<Rigidbody2D>().velocity.x < -.01f)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            return;
        }
    }

    public void PlayJumpAnimation()
    {
        anim.Play("Jump");
    }

    public void PlayDoubleJumpAnimation()
    {
        anim.Play("DoubleJump");
    }

    public void PlayBalanceAnimation()
    {
        anim.Play("Balance");
    }

    public void PlayDeathAnimation()
    {
        anim.Play("Death");
    }

    public void ResetAnimation(PlayerController playerController)
    {
        anim.Play("Balance");
    }

}
