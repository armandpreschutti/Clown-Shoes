
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBallHandler : MonoBehaviour
{
    [SerializeField] Sprite[] ballSprites;
    [SerializeField] Sprite currentBallSprite;

    private void OnEnable()
    {
        PlayerController.OnRespawn += SetRandomBall;
    }
    private void OnDisable()
    {
        PlayerController.OnRespawn -= SetRandomBall;
    }

    private void Start()
    {
        SetRandomBall(null);
    }
    public void SetRandomBall(PlayerController playerController)
    {
        int randomIndex = Random.Range(0, ballSprites.Length);

        // Use the random index to access an element in the array
        Sprite randomObject = ballSprites[randomIndex];


        Sprite newSprite = randomObject;
        GetComponent<SpriteRenderer>().sprite = newSprite; 
    }
}
