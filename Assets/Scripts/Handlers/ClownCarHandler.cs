using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownCarHandler : MonoBehaviour
{
    [SerializeField] GameObject car;
    [SerializeField] float carSpeed;
    [SerializeField] bool onBoard;
    [SerializeField] Vector3 originPoint;
    [SerializeField] float distance;
    private void Awake()
    {
        originPoint = car.transform.position;
    }

    private void OnEnable()
    {
        PlayerController.OnRespawn += ResetCar;
    }

    private void OnDisable()
    {
        PlayerController.OnRespawn -= ResetCar;
    }

    public void Update()
    {
        if(onBoard && car.transform.position.x < originPoint.x + distance)
        {
            car.transform.Translate(Vector2.right * carSpeed * Time.deltaTime);
        }
        else
        {
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player is on the car");
            onBoard = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player fell off the car");
            onBoard= false;
        }
    }

    public void ResetCar(PlayerController playerController)
    {
        car.transform.position = originPoint;
    }
}
