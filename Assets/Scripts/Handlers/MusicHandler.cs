using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicHandler : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip mainMenuMusic;
    public AudioClip clownCollegeMusic;
    public AudioClip postGameMusic;

    private void OnEnable()
    {
        FinishLineHandler.OnFinish += PlayPostGameMusic;
        SceneManager.sceneLoaded += PlayMainMenuMusic;
    }
    private void OnDisable()
    {
        FinishLineHandler.OnFinish -= PlayPostGameMusic;
        SceneManager.sceneLoaded -= PlayMainMenuMusic;
    }

    public void PlayPostGameMusic()
    {
        audioSource.clip= postGameMusic;
        audioSource.volume= 1.0f;
        audioSource.Play();
    }
    public void PlayMainMenuMusic(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "MainMenu")
        {
            audioSource.clip = mainMenuMusic;
            audioSource.volume = .1f;
            audioSource.Play();
        }
        
    }
}
