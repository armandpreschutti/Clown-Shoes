using System.Collections;
using UnityEngine;

public class HazardController : MonoBehaviour
{
    void Start()
    {
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.angularVelocity = 360f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player hit a pin");
            GameObject player = collision.gameObject;
            PlayerController playerController = player.GetComponent<PlayerController>();
            playerController.LaunchPlayer();
            /*if (playerController.IsAttachedToBall())
            {
                playerController.LaunchPlayer();
            }*/
        }
    }
}
