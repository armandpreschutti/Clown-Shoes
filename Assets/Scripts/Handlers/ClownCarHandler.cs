using DG.Tweening;
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
    private void Awake()
    {
        originPoint = transform.position;
        distance = originPoint.x + endPoint.localPosition.x;
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
        if (onBoard && transform.position.x < distance)
        {
            rb.velocity = new Vector2(carSpeed, 0f);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            if (collision.GetComponent<BallController>().isDriving)
            {
                Debug.Log("Player is on the car");
                onBoard = true;

            }
            else
            {
                Debug.Log("Player fell off the car");
                onBoard = false;
            }
        }
    }

    public void ResetCar(PlayerController playerController)
    {
        transform.position = originPoint;
    }
}
