using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject menuStuff;
    [SerializeField] public GameObject creditsStuff;

    public void OnPlayButton() {
        SceneManager.LoadScene("ClownCollegeLayout");
    
    }
    public void OnCreditsButton() {
        menuStuff.SetActive(false);
        creditsStuff.SetActive(true);

    }
    public void OnCreditsBackButton()
    {
        Debug.Log("back button pressed");
        creditsStuff.SetActive(false);
        menuStuff.SetActive(true);
    }

    public void OnExitButton(){
        Application.Quit();
    }
}
