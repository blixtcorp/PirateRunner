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
    public PlayerMovement pm;
    private float lastGeneratedY;
    private List<GameObject> chunks = new List<GameObject>();

    void Start()
    {
        lastGeneratedY = playerTransform.position.y + chunkSize + 10f;
        GenerateChunks(Vector3.zero);
    }

    void Update()
    {
        if (playerTransform.position.y >= lastGeneratedY - numChunksAhead * chunkSize)
        {
            //lastGeneratedY = playerTransform.position.y;

            Transform pathGenTransform = chunks[chunks.Count - 1].transform.Find("PathGenerator");
            if (pathGenTransform == null) { Debug.Log("pathGenTransform is null"); }

            PathGenerator pathGenerator = pathGenTransform.GetComponent<PathGenerator>();
            if (pathGenerator == null) { Debug.Log("pathGenerator is null"); }

            //while (!pathGenerator.stopGeneration) {} // wait until previous chunk is done generating
            //StartCoroutine(WaitUntilStopGeneration(pathGenerator));
            pm.movementSpeed += 0.3f;
            GenerateChunks(pathGenerator.endPos);
        }

        RemoveOldChunks();
    }

    void GenerateChunks(Vector3 startPosition)
    {
        //float startHeight = lastGeneratedY;
        for (int i = 0; i < numChunksAhead + 1; i++)
        {
            //float chunkY = startHeight + i * chunkSize;
            //Vector3 chunkPos = new Vector3(0f, chunkY, 0f);
            
            Vector3 chunkPos = new Vector3(0f, lastGeneratedY, 0f);
            lastGeneratedY += chunkSize;

            GameObject chunk = Instantiate(chunkPrefab, chunkPos, Quaternion.identity);
            chunks.Add(chunk);
            
            Transform pathGenTransform = chunk.transform.Find("PathGenerator");
            PathGenerator pathGenerator = pathGenTransform.GetComponent<PathGenerator>();
            
            pathGenerator.startPos = startPosition;
            //startPosition = pathGenerator.endPos;
            startPosition = pathGenerator.transform.TransformPoint(Vector3.zero);

            //while (!pathGenerator.stopGeneration) {} // wait until chunk is done generating
            //StartCoroutine(WaitUntilStopGeneration(pathGenerator));
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

    IEnumerator WaitUntilStopGeneration(PathGenerator pathGenerator) {
        while (!pathGenerator.stopGeneration) {
          yield return new WaitForEndOfFrame();
          Debug.Log("loading");
        }
        Debug.Log("Freedom");
        // Do something once path generation has stopped
    }
}
