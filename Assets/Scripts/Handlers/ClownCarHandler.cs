using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClownCarHandler : MonoBehaviour
{
    [SerializeField] GameObject car;
    [SerializeField] float carSpeed;
    [SerializeField] bool onBoard;
    [SerializeField] Vector3 originPoint;
    [SerializeField] float distance;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform endPoint;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clownCarSFX;
    public static Action<bool> OnClownCar;

    private void Awake()
    {
        originPoint = transform.position;
        distance = originPoint.x + endPoint.localPosition.x;
    }

    private void OnEnable()
    {
        PlayerController.OnRespawn += ResetCar;
        OnClownCar += PlayClownCarSFX;
    }

    private void OnDisable()
    {
        PlayerController.OnRespawn -= ResetCar;
        OnClownCar -= PlayClownCarSFX;
    }

    public void Update()
    {
        if (onBoard && transform.position.x < distance)
        {
            rb.velocity = new Vector2(carSpeed, 0f);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
/*    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball") && collision.GetComponent<BallController>().isDriving)
        {
            OnClownCar?.Invoke(true);

        }
    }*/
    
        

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
         
            onBoard = collision.GetComponent<BallController>().isDriving ? true : false;
            OnClownCar?.Invoke(onBoard);
        }
    }
  /*  private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball") && !collision.GetComponent<BallController>().isDriving)
        {

            onBoard = collision.GetComponent<BallController>().isDriving ? true : false;
            OnClownCar?.Invoke(false);

        }
    }
*/
    public void ResetCar(PlayerController playerController)
    {
        transform.position = originPoint;
    }
    public void PlayClownCarSFX(bool value)
    {
      /*  // Clone the original audio clip
        AudioClip clonedClip = UnityEngine.Object.Instantiate(clownCarSFX);

        // Calculate the new length of the audio clip based on the speed
        int newLength = (int)(clonedClip.samples / 2);

        // Create a new array to hold the modified audio data
        float[] newData = new float[newLength * clonedClip.channels];

        // Copy the modified audio data into the new array
        clonedClip.GetData(newData, 0);

        // Set the modified audio data to the cloned audio clip
        clonedClip.SetData(newData, 0);

        // Assign the modified audio clip to the AudioSource
        audioSource.clip = clonedClip;

        *//*// Play the modified audio clip
        audioSource.Play();*/

        if (value)
        {

            audioSource.enabled = true;
        }
        else
        {
            audioSource.enabled = false;
        }


    }
}
