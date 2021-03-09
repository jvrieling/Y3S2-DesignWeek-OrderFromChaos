using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public bool good = false;

    [ReadOnly] public int id;

    public AudioClip badSound;
    public AudioClip goodSound;

    public Color goodColour = Color.green;
    public Color badColour = Color.red;

    AudioSource audioSrc;
    SpriteRenderer sprtRndr;

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
        sprtRndr = GetComponent<SpriteRenderer>();
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
