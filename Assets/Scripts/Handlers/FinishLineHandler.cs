using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class FinishLineHandler : MonoBehaviour
{
    public static Action OnFinish;
    public SpriteRenderer celebration;
    public GameObject canvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnFinish?.Invoke();
            celebration.enabled = true; 
            canvas.SetActive(true);
        }
    }
    
    public void ReplayGame()
    {
        SceneManager.LoadScene("ClownCollegeLayout");
    }
}
