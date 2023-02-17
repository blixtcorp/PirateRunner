using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextChunk : MonoBehaviour
{
    public PathGenerator pathGen;
    public GameObject nextChunk;
    private GameObject chunkInstance;

    private Vector3 startPos;
    public bool isDone;

    void Start() {
        isDone = false;
    }

    void Update() {
        if (pathGen.stopGeneration == true && !isDone) {
            isDone = true;
            CreateNewChunk();
        }
    }

    void CreateNewChunk() {
        if (nextChunk == null) {
            Debug.LogError("Next chunk prefab is null!");
            return;
        }

        chunkInstance = Instantiate(nextChunk, transform.position + new Vector3(0,10,0), Quaternion.identity);

        Transform pathGenTransform = chunkInstance.transform.Find("PathGenerator");
        if (pathGenTransform == null) {
            Debug.LogError("PathGenerator subchild object not found on prefab!");
            return;
        }

        PathGenerator pathGenScript = pathGenTransform.GetComponent<PathGenerator>();
        if (pathGenScript == null) {
            Debug.LogError("PathGenerator component not found on subchild object!");
            return;
        }

        //Debug.Log("Before assignment: startPos = " + startPos + ", pathGenScript.startPos = " + pathGenScript.startPos);
        startPos = pathGen.transform.position;
        pathGenScript.startPos = new Vector3(startPos.x, startPos.y + 2.0f);
        //Debug.Log("After assignment: startPos = " + startPos + ", pathGenScript.startPos = " + pathGenScript.startPos);
    }
}
