using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFXHandler : MonoBehaviour
{
    public GameObject jumpVFX;

    private void OnEnable()
    {
        PlayerController.OnJump += PlayJumpVFX;
        PlayerController.OnDoubleJump += PlayJumpVFX;
    }
    private void OnDisable()
    {
        PlayerController.OnJump -= PlayJumpVFX;
        PlayerController.OnDoubleJump -= PlayJumpVFX;
    }

    public void PlayJumpVFX()
    {
        Instantiate(jumpVFX, transform);
    }
}
