using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SeesawCOMBias : MonoBehaviour
{
    [SerializeField] PlayerInput playerController;
    [SerializeField] public float bias;
    private Rigidbody2D rigidbody2D;

    private Quaternion initialRotation;

    void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        initialRotation = this.transform.rotation;
    }

    private void OnEnable()
    {
        PlayerController.OnRespawn += OnReset;
    }

    private void OnReset(PlayerController playerController)
    {
        rigidbody2D.angularVelocity = 0;
        transform.rotation = initialRotation;
    }


    private void FixedUpdate()
    {
        rigidbody2D.AddForceAtPosition(new Vector2(0, -bias), new Vector2(-1, 0));
    }

}
