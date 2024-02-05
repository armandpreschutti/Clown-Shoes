using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CourseTimeHandler : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Text to display the stopwatch time
    private float elapsedTime = 0f; // Elapsed time for the stopwatch
    [SerializeField] bool isRunning = false; // Flag to track if the stopwatch is running

    private void Update()
    {
        if (isRunning)
        {
            // Update the elapsed time
            elapsedTime += Time.deltaTime;

            // Update the display text
            UpdateTimerText();
        }
    }

    // Start the stopwatch
    public void StartStopwatch()
    {
        isRunning = true;
    }

    // Pause the stopwatch
    public void PauseStopwatch()
    {
        isRunning = false;
    }

    // Reset the stopwatch to zero
    public void ResetStopwatch()
    {
        elapsedTime = 0f;
        UpdateTimerText();
    }

    // Update the display text with the elapsed time
    private void UpdateTimerText()
    {
        int minutes = (int)(elapsedTime / 60f);
        int seconds = (int)(elapsedTime % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
