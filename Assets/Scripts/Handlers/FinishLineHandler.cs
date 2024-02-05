using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class FinishLineHandler : MonoBehaviour
{
    public static Action OnFinish;
    public SpriteRenderer celebration;
    public GameObject canvas;
    public GameObject courseTimer;
    public TextMeshProUGUI time;
    public CinemachineVirtualCamera vCam;
    public Transform camOffset;
    public EventSystem eventSystem;
    public GameObject button;
/*
    private void Start()
    {
        
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnFinish?.Invoke();
            celebration.enabled = true; 
            canvas.SetActive(true);
            time.text = courseTimer.GetComponent<CourseTimeHandler>().timerText.text;
            courseTimer.SetActive(false);
            vCam.Follow = camOffset;
            vCam.LookAt = camOffset;
            eventSystem.SetSelectedGameObject(button);
            

        }
    }
    
    public void ReplayGame()
    {
        SceneManager.LoadScene("ClownCollegeLayout");
    }
}
