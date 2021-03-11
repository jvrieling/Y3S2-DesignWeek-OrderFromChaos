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

    public Marker bar;

    private float timer;

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
            //stop all the nodes from playing sound        
            StopAllCoroutines();
            
            foreach (Node k in nodes)
            {
                k.GetComponent<AudioSource>().Stop();
            }

            //Start playing slots
            StartCoroutine(PlayAllNodes());
            playNodes = false;
        }        
        
        if(AccessibilityOption.accessibilityMode) blur.material.SetFloat("_Size", 50 - ((goodNodes / 11) * 50));

        if(goodNodes >= nodes.Count)
        {
            button.WinScreen();
        }
    }

    public IEnumerator PlayAllNodes()
    {
        //Loop through all the slots
        for (currentlyPlayingId = 0; currentlyPlayingId < slots.Count; currentlyPlayingId++) { 
            Slot i = slots[currentlyPlayingId];
            float maxTimeToPlay = 0;

            //for each slot, go through the nodes and play the ones within range
            foreach (Node j in nodes)
            {
                if (IsWithinXRange(j.transform.position.x, i.transform.position.x))
                {
                    AudioSource temp = j.GetComponent<AudioSource>();
                    if (temp.clip.length > maxTimeToPlay) maxTimeToPlay = temp.clip.length; //Set the max time to play to the LONGEST of the nodes in range.
                    temp.Play();
                }
            }

            bar.StartUpdateMarker(i.transform.position);

            //set timer for how long the PROPER sound takes to play
            timer = goodSounds[i.id].length + Time.deltaTime;

            //However, if timer is LONGER than the longest selected node, avoid the empty space and shorten timer to the longest selected node.
            if (timer > maxTimeToPlay) timer = maxTimeToPlay; 

            //Wait for timer to run out to give time for the clips to play.
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                yield return null;
            }

            //stop all the nodes from playing sound
            foreach(Node k in nodes)
            {
                k.GetComponent<AudioSource>().Stop();
            }

            Debug.Log("Done playing from slot " + i.name + "!");
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

    public void SkipSlot()
    {
        timer = 0;
    }
    public void PrevSlot()
    {
        currentlyPlayingId -= 2;
        timer = 0;
    }
}