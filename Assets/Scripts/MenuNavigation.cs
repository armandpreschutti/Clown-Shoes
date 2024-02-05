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
    [SerializeField] public GameObject creditsFirst;
    [SerializeField] public GameObject menuFirst;
    public EventSystem eventSystem;

    private void Start()
    {
        eventSystem.SetSelectedGameObject(menuFirst);
    }
    public void OnPlayButton() {
        SceneManager.LoadScene("ClownCollegeLayout");
    
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

    public void OnExitButton(){
        Application.Quit();
    }
}
