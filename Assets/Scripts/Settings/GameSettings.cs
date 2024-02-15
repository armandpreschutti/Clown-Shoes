using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using DG.Tweening.Core.Easing;

public class GameSettings : MonoBehaviour
{

    public static event Action<string> OnCycleDifficulty;
    public static event Action<bool> OnToggleMusic;
    public static event Action<bool> OnToggleSFX;
    public string difficulty;
    public bool musicEnabled;
    public bool sfxEnabled;
    private GameManager gameManager;

    private void OnEnable()
    {
        gameManager = GameManager.GetInstance();
        SetInitialSettings();
    }
    public void CycleDifficulty()
    {
        switch (difficulty)
        {
            case "easy":
                difficulty = "normal";
                break;
            case "normal":
                difficulty = "hard";
                break;
            case "hard":
                difficulty = "easy";
                break;
            default:
                Debug.LogError("There is a problem in your Game Settings");
                break;
        }
        GameObject.Find("DifficultyButton").GetComponent<TextMeshProUGUI>().text = difficulty.FirstCharacterToUpper();
        OnCycleDifficulty?.Invoke(difficulty);
    }
    public void ToggleMusic()
    {
        musicEnabled = !musicEnabled;
        OnToggleMusic?.Invoke(musicEnabled);
    }
    public void ToggleSFX()
    {
        sfxEnabled= !sfxEnabled;
        OnToggleSFX?.Invoke(sfxEnabled);
    }

    public void SetInitialSettings()
    {
        difficulty = gameManager.difficultySetting;
        GameObject.Find("MusicButton").GetComponent<Toggle>().isOn = gameManager.musicSetting;
        GameObject.Find("SFXButton").GetComponent<Toggle>().isOn = gameManager.sfxSetting;
        GameObject.Find("DifficultyButton").GetComponent<TextMeshProUGUI>().text = difficulty.FirstCharacterToUpper();
    }
}
