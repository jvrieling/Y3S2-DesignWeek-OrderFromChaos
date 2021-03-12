using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public bool good = false;
    public bool updateColour = false;

    public int id;
    public bool contained = false;


    public Color goodColour = Color.green;
    public Color badColour = Color.red;

    public static int snapLeeway = 15;
    public static int segmentWidth = 70;

    public SlotMap map;

    public Node containedNode;

    Image img;

    private void Awake()
    {
        img = GetComponent<Image>();
    }

    private void Update()
    {
        if (updateColour)
        {
            if (good)
            {
                img.color = goodColour;
            }
            else
            {
                img.color = badColour;
            }
        }

        foreach (Node i in SlotMap.instance.nodes)
        {
            if (IsWithinRange(transform.position, i.transform.position))
            {
                if (i.currentlyBeingDragged == false)
                {
                    contained = true;
                    containedNode = i;
                }
            }
            else
            {
                contained = false;
                if (containedNode != null)
                {
                    if (containedNode.id == i.id && containedNode.id == id)
                    {
                        SlotMap.instance.goodNodes--;
                        containedNode = null;
                    }
                }
            }
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(new Vector3(transform.position.x - segmentWidth, 10000, transform.position.z), new Vector3(transform.position.x - segmentWidth, -10000, transform.position.z));
        Gizmos.DrawLine(new Vector3(transform.position.x + segmentWidth, 10000, transform.position.z), new Vector3(transform.position.x + segmentWidth, -10000, transform.position.z));
        Gizmos.color = Color.white;
    }

    public void OnDrop(PointerEventData eventData)
    {

        if (eventData.pointerDrag != null)
        {
            if (!contained)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                contained = true;
            }

        }
    }
}
