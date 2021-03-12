using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class appearingButtons : MonoBehaviour
{

    public GameObject button1;
    public GameObject continueObject;
    public Text continueText;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Show (13.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Show (float delay)
    {
        timer = delay;
        button1.SetActive(false);
        do
        {
            continueText.text = timer + "";
            timer--;
            yield return new WaitForSeconds(1);
        } while (timer > 0);
        continueObject.SetActive(false);
        button1.SetActive(true);
    }
}
