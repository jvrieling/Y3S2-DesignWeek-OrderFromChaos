using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMap : MonoBehaviour
{
    public static SlotMap instance;

    public List<GameObject> nodes;

    public List<AudioClip> goodSounds;
    public List<AudioClip> badSounds;

    //public GameObject nodePrefab;

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
        for(int i = 0; i < nodes.Count; i++)
        {
            Slot tempNode = nodes[i].GetComponent<Slot>();

            tempNode.goodSound = goodSounds[i];
            tempNode.badSound = badSounds[i];

            tempNode.id = i;
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
        foreach(GameObject i in nodes)
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

    public GameObject PlaceNote(int id, Vector2 pos)
    {
        foreach (GameObject i in nodes)
        {
            if(IsWithinRange(i.transform.localPosition, pos))
            {
                Slot tempNode = i.GetComponent<Slot>();

                if(id == tempNode.id)
                {
                    tempNode.good = true;
                    goodNodes++;
                }

                return i;
            }
        }
        return null;
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
