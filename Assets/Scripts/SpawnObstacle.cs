using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    public LayerMask whatIsPath;
    public PathGenerator pathGen;
    private bool isDone = false;
    public bool isEligible = false;

    void Update() {
        Collider[] pathsDetected = Physics.OverlapBox(transform.position, new Vector3(1,1,1), Quaternion.identity, whatIsPath);
        if (pathsDetected.Length == 0 && pathGen.stopGeneration == true && !isDone) { // No path blocking our way, spawn obstacle
            isDone = true;
            isEligible = true;
        } else if (pathsDetected.Length > 0) { // Path blocking our way, no need to spawn obstacle
            isDone = true;
        }
    }

    public void instantiateObstacle() {
        int rand = Random.Range(0, pathGen.obstacles.Length);
        Instantiate(pathGen.obstacles[rand], transform.position, Quaternion.identity, this.transform);
    }
}
