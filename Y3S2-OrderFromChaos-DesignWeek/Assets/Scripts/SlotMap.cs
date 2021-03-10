using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMap : MonoBehaviour
{
    public static SlotMap instance;

    public List<Slot> slots;
    public List<Node> nodes;
    public bool autoAssignNodeIds = false;

    public List<AudioClip> goodSounds;
    public List<AudioClip> badSounds;

    public bool isAllowed;

    public float nodeCount = 10;
    public float nodeDistanceBuffer = 1.5f;

    public bool playNodes = false;

    public int currentlyPlayingId;

    public float goodNodes;
    public Image blur;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            Slot tempSlot = slots[i];
            tempSlot.id = i;
        }
        for (int i = 0; i < nodes.Count; i++)
        {
            Node tempNode = nodes[i];

            if (autoAssignNodeIds) tempNode.id = i;

            tempNode.goodSound = goodSounds[i];
            tempNode.badSound = badSounds[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playNodes)
        {
            StopAllCoroutines();
            StartCoroutine(PlayAllNodes());
            playNodes = false;
        }

        //blur.sharedMaterial.SetFloat("_Size", (goodNodes/11)*20);
        blur.material.SetFloat("_Size", 20 - ((goodNodes / 11) * 20));
    }

    public IEnumerator PlayAllNodes()
    {
        currentlyPlayingId = 0;
        foreach (Slot i in slots)
        {
            AudioSource temp = i.GetComponent<AudioSource>();
            temp.Play();
            do
            {
                yield return null;
            } while (temp.isPlaying);
            Debug.Log("Done! " + i.name);
            currentlyPlayingId++;
        }
    }

    public bool PlaceNote(int id, Vector2 pos)
    {
        foreach (Slot i in slots)
        {
            if (IsWithinRange(i.transform.localPosition, pos))
            {

                if (id == i.id)
                {
                    i.good = true;
                    goodNodes++;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return false;
    }

    private bool IsWithinRange(Vector2 original, Vector2 inQuestion)
    {
        return (inQuestion.x < original.x + Slot.snapLeeway && inQuestion.x > original.x - Slot.snapLeeway && inQuestion.y < original.y + Slot.snapLeeway && inQuestion.y > original.y - Slot.snapLeeway);
    }

    public void PlayNodes()
    {
        playNodes = true;
    }

    private void OnDisable()
    {
        blur.material.SetFloat("_Size", 0);
    }
}
