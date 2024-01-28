using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationZone : MonoBehaviour
{

    [SerializeField] public Transform direction;
    private Vector3 boost;
    private List<GameObject> boostedObjects;

    // Start is called before the first frame update
    void Start()
    {
        boostedObjects = new List<GameObject>();
        boost =  (direction.position - this.transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (GameObject obj in boostedObjects) {
            obj.GetComponent<Rigidbody2D>().AddForce(boost);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            Debug.Log("Ball being boosted");
            boostedObjects.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Ball stopped being boosted");
        boostedObjects.Remove(collision.gameObject);
    }
}
