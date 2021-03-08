using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMap : MonoBehaviour
{

    public List<GameObject> nodes;

    public GameObject nodePrefab;

    public float nodeCount = 10;
    public float nodeDistanceBuffer = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 currentPosition = transform.position;
        for(int i = 0; i < nodeCount; i++)
        {
            GameObject temp = Instantiate(nodePrefab, currentPosition, Quaternion.identity);
            nodes.Add(temp);

            currentPosition.x += nodeDistanceBuffer;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
