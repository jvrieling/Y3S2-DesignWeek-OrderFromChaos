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
        blur.material.SetFloat("_Size", 50 - ((goodNodes / 11) * 50));
    }

    public IEnumerator PlayAllNodes()
    {
        currentlyPlayingId = 0;
        //Loop through all the slots
        foreach (Slot i in slots)
        {
            //for each slot, go through the nodes and play the ones within range
            foreach (Node j in nodes)
            {
                if (IsWithinXRange(j.transform.position.x, i.transform.position.x))
                {
                    AudioSource temp = j.GetComponent<AudioSource>();
                    temp.Play();
                }
            }

            //Wait for how long the PROPER sound takes to play
            yield return new WaitForSeconds(goodSounds[i.id].length);

            //stop all the nodes from playing sound
            foreach(Node k in nodes)
            {
                k.GetComponent<AudioSource>().Stop();
            }

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
    private bool IsWithinXRange(float original, float inQuestion)
    {
        return (inQuestion < original + Slot.segmentWidth && inQuestion > original - Slot.segmentWidth);
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
