using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HazardJuggler : MonoBehaviour
{
    [SerializeField] public GameObject hazardPrefab;
    [SerializeField] public float interval;
    [SerializeField] public float expiry = 10f;
    [SerializeField] public float height;
    [SerializeField] public float delay = 0f;

    [SerializeField] public List<Tuple<GameObject, float>> activeHazards;

    void ThrowHazard()
    {
        GameObject newHazard = GameObject.Instantiate(hazardPrefab, this.transform);
        activeHazards.Add(new Tuple<GameObject, float>(newHazard, Time.timeSinceLevelLoad));
        newHazard.GetComponent<Rigidbody2D>().velocity = Vector3.up * height;
    }

    void Start()
    {
        activeHazards = new List<Tuple<GameObject, float>>();
        InvokeRepeating("ThrowHazard", interval+ delay, interval);

    }

    void Update()
    {
        float currentTime = Time.timeSinceLevelLoad;
        foreach (Tuple<GameObject, float> tuple in activeHazards) {
            if (currentTime - tuple.Item2 > expiry) {
                GameObject.Destroy(tuple.Item1);
            }
        }
        activeHazards.RemoveAll(tuple => currentTime - tuple.Item2 > expiry);
    }

}
