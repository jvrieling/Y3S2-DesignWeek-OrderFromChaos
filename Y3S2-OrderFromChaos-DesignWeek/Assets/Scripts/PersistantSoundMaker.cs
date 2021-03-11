using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantSoundMaker : MonoBehaviour
{
    AudioSource s;
    public AudioClip buttonSound;
    public bool scenePersistant = true;
    public bool destroyAfterPlay = true;

    void Awake()
    {
        if (scenePersistant) DontDestroyOnLoad(this);

        s = GetComponent<AudioSource>();
    }

    public void PlayButtonSound()
    {
        s.PlayOneShot(buttonSound);
        if(destroyAfterPlay) Invoke("Delete", 1);
    }
    public void Delete()
    {
        Destroy(gameObject);
    }
}
