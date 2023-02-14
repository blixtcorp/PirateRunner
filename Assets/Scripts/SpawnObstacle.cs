using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    public LayerMask whatIsPath;
    public PathGenerator pathGen;

    void Update() {
        Collider2D pathDetection = Physics2D.OverlapCircle(transform.position, 1, whatIsPath);
        if (pathDetection == null && pathGen.stopGeneration == true) { // No path blocking our way, spawn obstacle
            int rand = Random.Range(0, pathGen.obstacles.Length);
            Instantiate(pathGen.obstacles[rand], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
