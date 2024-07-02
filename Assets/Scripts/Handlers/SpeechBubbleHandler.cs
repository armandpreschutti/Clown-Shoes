using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubbleHandler : MonoBehaviour
{

    [SerializeField] GameObject speechBubble;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnableBubble());
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator EnableBubble()
    {
        yield return new WaitForSeconds(1);
        speechBubble.SetActive(true);
    }
   
}
