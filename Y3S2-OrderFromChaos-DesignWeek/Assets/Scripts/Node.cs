using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    public bool good = false;

    public int id;

    public AudioClip badSound;
    public AudioClip goodSound;

    public Color goodColour = Color.green;
    public Color badColour = Color.red;

    public static int snapLeeway = 20;

    public NodeMap map;

    AudioSource audioSrc;
    Image img;

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
        img = GetComponent<Image>();
    }

    private void Update()
    {


        if (good)
        {
            audioSrc.clip = goodSound;
            img.color = goodColour;
        }
        else
        {
            audioSrc.clip = badSound;
            img.color = badColour;
        }
    }

    private bool IsWithinRange(Vector2 original, Vector2 inQuestion)
    {

        return (inQuestion.x < original.x + snapLeeway && inQuestion.x > original.x - snapLeeway && inQuestion.y < original.y + snapLeeway && inQuestion.y > original.y - snapLeeway);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        Vector3 origin = new Vector3(transform.position.x + snapLeeway, transform.position.y + snapLeeway, transform.position.z);
        Vector3 end = new Vector3(transform.position.x + snapLeeway, transform.position.y - snapLeeway, transform.position.z);

        Gizmos.DrawLine(origin, end);

        origin = new Vector3(transform.position.x - snapLeeway, transform.position.y - snapLeeway, transform.position.z);
        end = new Vector3(transform.position.x + snapLeeway, transform.position.y - snapLeeway, transform.position.z);

        Gizmos.DrawLine(origin, end);

        origin = new Vector3(transform.position.x + snapLeeway, transform.position.y + snapLeeway, transform.position.z);
        end = new Vector3(transform.position.x - snapLeeway, transform.position.y + snapLeeway, transform.position.z);

        Gizmos.DrawLine(origin, end);

        origin = new Vector3(transform.position.x - snapLeeway, transform.position.y - snapLeeway, transform.position.z);
        end = new Vector3(transform.position.x - snapLeeway, transform.position.y + snapLeeway, transform.position.z);

        Gizmos.DrawLine(origin, end);

        Gizmos.color = Color.white;
    }
}
