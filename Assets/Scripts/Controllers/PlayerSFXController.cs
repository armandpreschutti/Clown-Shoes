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
        PauseController.OnPause += PauseSFX;
    }

    private void OnDisable()
    {
        PlayerController.OnJump -= PlayJumpSFX;
        PlayerController.OnRespawn -= PlayRespawnSFX;
        PlayerController.OnGround -= PlayFallSFX;
        FinishLineHandler.OnFinish -= PlayVictorySFX;
        BalanceController.OnFall -= PlayFallSFX;
        PauseController.OnPause -= PauseSFX;
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
    public void PauseSFX(bool value)
    {
        if(value)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }

    }
}
