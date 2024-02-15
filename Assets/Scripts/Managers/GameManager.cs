using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager gameInstance;

    [Header("Game Settings")]
    public string difficultySetting;
    public bool musicSetting;
    public bool sfxSetting;

    [Header("Game Elements")]
    public GameObject music;
    public GameObject sfx;


    public static GameManager GetInstance()
    {
        return gameInstance;
    }

    private void Awake()
    {
        if (gameInstance == null)
        {
            gameInstance = this;
            InitializeGameElements();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        GameSettings.OnCycleDifficulty += ChangeDifficulty;
        GameSettings.OnToggleMusic += ChangeMusicEnabled;
        GameSettings.OnToggleSFX += ChangeSFXEnabled;
    }

    private void OnDisable()
    {
        GameSettings.OnCycleDifficulty -= ChangeDifficulty;
        GameSettings.OnToggleMusic -= ChangeMusicEnabled;
        GameSettings.OnToggleSFX -= ChangeSFXEnabled;
    }

    private void ChangeDifficulty(string value)
    {
        difficultySetting = value;        
    }

    private void ChangeMusicEnabled(bool value)
    {
        musicSetting= value;
        music.SetActive(value);
    }

    private void ChangeSFXEnabled(bool value)
    {
        sfxSetting= value;
        sfx.SetActive(value);
    }  
    
    public void InitializeGameElements()
    {
        music.SetActive(musicSetting);
        sfx.SetActive(sfxSetting);
    }


}
