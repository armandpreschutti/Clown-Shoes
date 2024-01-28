using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float length, startpos;
    [SerializeField] public float parallaxFactor;
    public GameObject cam;
    private float initialYOffset = 0;

    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        initialYOffset = transform.position.y - cam.transform.position.y;
    }

    void Update()
    {
        float temp = cam.transform.position.x * (1 - parallaxFactor);
        float distance = cam.transform.position.x * parallaxFactor;

        Vector3 newPosition = new Vector3(startpos + distance, cam.transform.position.y+ initialYOffset, transform.position.z);

        transform.position = newPosition;

        if (temp > startpos + (length / 2)) startpos += length;
        else if (temp < startpos - (length / 2)) startpos -= length;
    }
}