using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFXController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip boo;
    public AudioClip squel;
    public AudioClip whistle;
    public AudioClip victory;


    private void OnEnable()
    {
        PlayerController.OnJump += PlayJumpSFX;
        PlayerController.OnRespawn += PlayRespawnSFX;
        PlayerController.OnGround += PlayFallSFX;
        FinishLineHandler.OnFinish += PlayVictorySFX;
        BalanceController.OnFall += PlayFallSFX;        
    }

    private void OnDisable()
    {
        PlayerController.OnJump -= PlayJumpSFX;
        PlayerController.OnRespawn -= PlayRespawnSFX;
        PlayerController.OnGround -= PlayFallSFX;
        FinishLineHandler.OnFinish -= PlayVictorySFX;
        BalanceController.OnFall -= PlayFallSFX;
    }

    public void PlayRespawnSFX(PlayerController playerController)
    {
        audioSource.clip = squel;
        audioSource.Play();
    }

    public void PlayVictorySFX()
    {
        audioSource.clip = victory;
        audioSource.Play();
    }

    public void PlayJumpSFX()
    {
        audioSource.clip = whistle;
        audioSource.Play();
    }

    public void PlayFallSFX()
    {
        audioSource.clip = boo;
        audioSource.Play();
    }
}
