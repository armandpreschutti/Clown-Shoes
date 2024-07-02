using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSFXController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip boo;
    public AudioClip squel;
    public AudioClip whistle;
    public AudioClip victory;
    public AudioClip rolling;
    public AudioClip clownCar;

    private void OnEnable()
    {
        PlayerController.OnJump += PlayJumpSFX;
        PlayerController.OnRespawn += PlayRespawnSFX;
        PlayerController.OnGround += PlayFallSFX;
        FinishLineHandler.OnFinish += PlayVictorySFX;
        BalanceController.OnFall += PlayFallSFX;
        PauseController.OnPause += PauseSFX;
        PlayerController.OnLand += PlayRollingSFX;
        ClownCarHandler.OnClownCar += PlayClownCarSFX;
        SceneManager.sceneLoaded += ResetSFX;
    }

    private void OnDisable()
    {
        PlayerController.OnJump -= PlayJumpSFX;
        PlayerController.OnRespawn -= PlayRespawnSFX;
        PlayerController.OnDeath -= PlayFallSFX;
        FinishLineHandler.OnFinish -= PlayVictorySFX;
        BalanceController.OnFall -= PlayFallSFX;
        PauseController.OnPause -= PauseSFX;
        PlayerController.OnLand -= PlayRollingSFX;
        ClownCarHandler.OnClownCar -= PlayClownCarSFX;
        SceneManager.sceneLoaded -= ResetSFX;
    }

    public void PlayRespawnSFX(PlayerController playerController)
    {
        audioSource.loop = false;
        audioSource.clip = squel;
        audioSource.Play();
    }

    public void PlayVictorySFX()
    {
        audioSource.loop = false;
        audioSource.clip = victory;
        audioSource.Play();
    }

    public void PlayJumpSFX()
    {
        audioSource.loop = false;
        audioSource.clip = whistle;
        audioSource.Play();
    }

    public void PlayFallSFX()
    {
        audioSource.loop = false;
        audioSource.clip = boo;
        audioSource.Play();
    }

    public void PlayRollingSFX()
    {
        audioSource.loop = true;
        audioSource.clip = rolling;
        audioSource.Play();
    }
    public void PlayClownCarSFX(bool value)
    {
        if (value)
        {
            audioSource.loop = false;
            audioSource.clip = clownCar;

        }
        else
        {
            audioSource.Stop();
        }
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
    public void ResetSFX(Scene scene, LoadSceneMode mode)
    {
        audioSource.Stop();
        audioSource.clip = null;
    }

}
