using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip graduation;

    private void OnEnable()
    {
        FinishLineHandler.OnFinish += PlayGraduationMusic;
    }
    private void OnDisable()
    {
        FinishLineHandler.OnFinish -= PlayGraduationMusic;
    }

    public void PlayGraduationMusic()
    {
        audioSource.clip= graduation;
        audioSource.volume= 1.0f;
        audioSource.Play();
    }
}
