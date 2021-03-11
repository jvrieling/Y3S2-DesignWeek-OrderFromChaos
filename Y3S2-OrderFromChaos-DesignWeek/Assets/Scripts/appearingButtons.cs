using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class appearingButtons : MonoBehaviour
{

    public GameObject button1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Show (2.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Show (float delay)
    {
        button1.SetActive (false);
        yield return new WaitForSeconds(delay);
        button1.SetActive(true);
    }
}
