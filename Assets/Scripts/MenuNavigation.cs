using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject menuPanel;
    [SerializeField] public GameObject creditsPanel;
    [SerializeField] public GameObject settingsPanel;
    [SerializeField] public GameObject creditsFirst;
    [SerializeField] public GameObject menuFirst;
    [SerializeField] public GameObject settingsFirst;

    public EventSystem eventSystem;

    private void Start()
    {
        eventSystem.SetSelectedGameObject(menuFirst);
    }
    public void OnPlayButton() {
        SceneManager.LoadScene("ClownCollege");
    
    }
    public void OnCreditsButton() {
        menuPanel.SetActive(false);
        creditsPanel.SetActive(true);
        eventSystem.SetSelectedGameObject(creditsFirst);
    }
    public void OnCreditsBackButton()
    { 
        creditsPanel.SetActive(false);
        menuPanel.SetActive(true);
        eventSystem.SetSelectedGameObject(menuFirst);
    }
    public void OnSettingsButton()
    {
        menuPanel.SetActive(false);
        settingsPanel.SetActive(true);
        eventSystem.SetSelectedGameObject(settingsFirst);
    }
    public void OnSettingsBackButton()
    {
        settingsPanel.SetActive(false);
        menuPanel.SetActive(true);
        eventSystem.SetSelectedGameObject(menuFirst);
    }

    public void OnExitButton(){
        Application.Quit();
    }
}
