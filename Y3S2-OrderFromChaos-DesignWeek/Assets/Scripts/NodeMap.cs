using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMap : MonoBehaviour
{

    public List<GameObject> nodes;

    public List<AudioClip> goodSounds;
    public List<AudioClip> badSounds;

    public GameObject nodePrefab;

    public float nodeCount = 10;
    public float nodeDistanceBuffer = 1.5f;

    public bool playNodes = false;

    [ReadOnly] public int currentlyPlayingId;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 currentPosition = transform.position;
        for(int i = 0; i < nodeCount; i++)
        {
            GameObject temp = Instantiate(nodePrefab, currentPosition, Quaternion.identity);

            temp.name = "Node " + i;

            nodes.Add(temp);

            Node tempNode = temp.GetComponent<Node>();

            tempNode.goodSound = goodSounds[i];
            tempNode.badSound = badSounds[i];

            tempNode.id = i;

            currentPosition.x += nodeDistanceBuffer;
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
}
