using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public bool good = false;

    public int id;
    public bool contained = false;

    public AudioClip badSound;
    public AudioClip goodSound;

    public Color goodColour = Color.green;
    public Color badColour = Color.red;

    public static int snapLeeway = 20;

    public SlotMap map;

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

    public void OnDrop(PointerEventData eventData)
    {

        if (eventData.pointerDrag != null)
        {
            if (!contained)
            {
                if (eventData.pointerDrag.GetComponent<Node>().id == id)
                {
                    eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                    contained = true;
                }
            }

        }
    }
}
