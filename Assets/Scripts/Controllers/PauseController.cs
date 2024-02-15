using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] InputAction pauseInput;
    public bool isPaused;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] EventSystem eventSystem;
    [SerializeField] GameObject resumeButton;
    [SerializeField] GameObject restartButton;
    [SerializeField] GameObject settingsButton;
    [SerializeField] GameObject mainMenuButton;
    [SerializeField] GameObject pauseButton;

    public static Action<bool> OnPause;

    private void Awake()
    {
        playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        pauseInput = playerInput.actions["Pause"];
    }
    private void OnEnable()
    {
        pauseInput.performed += PauseGame;
        FinishLineHandler.OnFinish += DeactivatePause;
    }
    private void OnDisable()
    {
        pauseInput.performed -= PauseGame;
        FinishLineHandler.OnFinish -= DeactivatePause;
    }

    public void PauseGame(InputAction.CallbackContext context)
    {
        TogglePauseMenu();

    }

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        OnPause?.Invoke(isPaused);
        if (isPaused)
        {

            Time.timeScale = 0.0f;
            pauseMenu.SetActive(true);
            eventSystem.SetSelectedGameObject(resumeButton);
            pauseButton.SetActive(false);
        }
        else
        {
            Time.timeScale = 1.0f;
            pauseMenu.SetActive(false);
            pauseButton.SetActive(true);
        }
    }
    public void ResumeGame()
    {
        TogglePauseMenu();
    }
    public void RestartLevel()
    {
        TogglePauseMenu();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ReturnToMainMenu()
    {
        TogglePauseMenu();
        SceneManager.LoadScene("MainMenu");
    }
    public void DeactivatePause()
    {
        pauseButton.SetActive(false);
    }
}
