using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public GameObject chunkPrefab; // change to array with different types of chunks
    public float chunkSize = 10f;
    public int numChunksAhead = 5;
    public float chunkDistanceBehind = 20f;

    public Transform playerTransform;
    private float lastGeneratedY;
    private List<GameObject> chunks = new List<GameObject>();

    void Start()
    {
        lastGeneratedY = playerTransform.position.y + chunkSize;
        GenerateChunks(Vector3.zero);
    }

    void Update()
    {
        if (playerTransform.position.y >= lastGeneratedY + chunkSize)
        {
            lastGeneratedY = playerTransform.position.y;
            Transform pathGenTransform = chunks[chunks.Count - 1].transform.Find("PathGenerator");
            PathGenerator pathGenerator = pathGenTransform.GetComponent<PathGenerator>();
            GenerateChunks(pathGenerator.endPos);
        }

        RemoveOldChunks();
    }

    void GenerateChunks(Vector3 startPosition)
    {
        float startHeight = lastGeneratedY;

        for (int i = 0; i < numChunksAhead + 1; i++)
        {
            float chunkY = startHeight + i * chunkSize;
            Vector3 chunkPos = new Vector3(0f, chunkY, 0f);

            GameObject chunk = Instantiate(chunkPrefab, chunkPos, Quaternion.identity);
            chunks.Add(chunk);
            
            Transform pathGenTransform = chunk.transform.Find("PathGenerator");
            PathGenerator pathGenerator = pathGenTransform.GetComponent<PathGenerator>();
            
            pathGenerator.startPos = startPosition;
            startPosition = pathGenerator.endPos;
        }
    }

    void RemoveOldChunks()
    {
        List<GameObject> chunksToRemove = new List<GameObject>();
        foreach (GameObject chunk in chunks)
        {
            if (playerTransform.position.y - chunk.transform.position.y > chunkDistanceBehind)
            {
                chunksToRemove.Add(chunk);
                Destroy(chunk);
            }
        }
        foreach (GameObject chunk in chunksToRemove)
        {
            chunks.Remove(chunk);
        }
    }
}
