using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMap : MonoBehaviour
{
    public static NodeMap instance;

    public List<GameObject> nodes;

    public List<AudioClip> goodSounds;
    public List<AudioClip> badSounds;

    //public GameObject nodePrefab;

    public float nodeCount = 10;
    public float nodeDistanceBuffer = 1.5f;

    public bool playNodes = false;

    public int currentlyPlayingId;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < nodes.Count; i++)
        {
            Node tempNode = nodes[i].GetComponent<Node>();

            tempNode.goodSound = goodSounds[i];
            tempNode.badSound = badSounds[i];

            tempNode.id = i;
        }
        /*Vector3 currentPosition = new Vector3();
        for(int i = 0; i < nodeCount; i++)
        {
            GameObject temp = Instantiate(nodePrefab, transform);

            temp.name = "Node " + i;

            RectTransform tempRectTransform = temp.GetComponent<RectTransform>();

            tempRectTransform.localPosition = Vector3.zero;

            tempRectTransform.Translate(currentPosition);
            
            nodes.Add(temp);

            Node tempNode = temp.GetComponent<Node>();

            tempNode.goodSound = goodSounds[i];
            tempNode.badSound = badSounds[i];

            tempNode.id = i;

            currentPosition.x += nodeDistanceBuffer;
        }*/
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
                Node tempNode = i.GetComponent<Node>();

                if(id == tempNode.id)
                {
                    tempNode.good = true;
                }

                return i;
            }
        }
        return null;
    }

    private bool IsWithinRange(Vector2 original, Vector2 inQuestion)
    {
        return (inQuestion.x < original.x + Node.snapLeeway && inQuestion.x > original.x - Node.snapLeeway && inQuestion.y < original.y + Node.snapLeeway && inQuestion.y > original.y - Node.snapLeeway);
    }
}
