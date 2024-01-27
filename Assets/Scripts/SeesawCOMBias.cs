using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeesawCOMBias : MonoBehaviour
{
    public float bias;
    private Rigidbody2D rigidbody2D;

    void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rigidbody2D.AddForceAtPosition(new Vector2(0, -bias), new Vector2(-1, 0));
    }

}
