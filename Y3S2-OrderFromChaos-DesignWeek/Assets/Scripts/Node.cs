using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    public bool good = false;

    [ReadOnly] public int id;

    public AudioClip badSound;
    public AudioClip goodSound;

    public Color goodColour = Color.green;
    public Color badColour = Color.red;

    AudioSource audioSrc;
    Image sprtRndr;

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
        sprtRndr = GetComponent<Image>();
    }

    private void Update()
    {
        if (good)
        {
            audioSrc.clip = goodSound;
            sprtRndr.color = goodColour;
        } else
        {
            audioSrc.clip = badSound;
            sprtRndr.color = badColour;
        }
    }

    public void OnMouseDown()
    {
        good = !good;
    }
}
