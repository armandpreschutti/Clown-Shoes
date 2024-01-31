using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject menuPanel;
    [SerializeField] public GameObject creditsPanel;

    public void OnPlayButton() {
        SceneManager.LoadScene("ClownCollegeLayout");
    
    }
    public void OnCreditsButton() {
        menuPanel.SetActive(false);
        creditsPanel.SetActive(true);

    }
    public void OnCreditsBackButton()
    {
        Debug.Log("back button pressed");
        creditsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void OnExitButton(){
        Application.Quit();
    }
}
