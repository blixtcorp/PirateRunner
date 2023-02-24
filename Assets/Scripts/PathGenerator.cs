using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathGenerator : MonoBehaviour
{
    public Transform[] startingPositions; // Possible starting positions for path generation
    public Transform[] extremePoints; // max min values for x and y
    private float minX, maxX, minY, maxY; 
    
    public GameObject[] paths; // Array of objects that can be spawned on the 'path'
    public int[] weights; 
    private int totalWeight;

    public GameObject[] obstacles; // Array of obstacles that can be spawned where there is no path

    private List<GameObject> pathChildren = new List<GameObject>(); // List containing instanciated children

    private int direction;
    public bool stopGeneration;

    public float moveIncrement;
    private float timeBtwSpawn;
    public float startTimeBtwSpawn;

    public LayerMask whatIsPath; 

    public Vector3 startPos;
    public Vector3 endPos;

    private void Start()
    {
        // If there's no previous chunk, generate a random starting position, 
        // otherwise make use of the previous chunk's finishing position
        if (startPos == new Vector3(0,0,0)) {
            Debug.Log("no previous starting pos: " + startPos);
            int randStartingPos = Random.Range(0, startingPositions.Length);
            transform.position = startingPositions[randStartingPos].position;
        } 
        else {
            Debug.Log("Given starting pos: " + startPos);
            transform.position = startPos;
        }
        
        minX = extremePoints[0].position.x;
        maxX = extremePoints[1].position.x;
        minY = extremePoints[0].position.y;
        maxY = extremePoints[1].position.y;

        randomPathObject();

        // Random direction for the path generator
        direction = Random.Range(1, 7);

        totalWeight = weights.Sum();
    }

    private void Update()
    {
        if (timeBtwSpawn <= 0 && stopGeneration == false) {
            Move();
            timeBtwSpawn = startTimeBtwSpawn;
        } 
        else {
            timeBtwSpawn -= Time.deltaTime;
        }

        if (stopGeneration) { // Makes all paths children of the pathgenerator gameobject. This is to later efficently be able to delete everything at once.
            foreach (GameObject pc in pathChildren) {
                if (pc == null) {break;}
                pc.transform.parent = transform;
            }
        }
    }

    private void Move() {
        if (direction == 1 || direction == 2) { // Move pathgenerator to the right
            if (transform.position.x < maxX) {
                Vector2 pos = new Vector2(transform.position.x + moveIncrement, transform.position.y);
                transform.position = pos;

                randomPathObject();
            }
            direction = 5;
        } 
        else if (direction == 3 || direction == 4) { // Move pathgenerator to the left
            if (transform.position.x > minX) {
                Vector2 pos = new Vector2(transform.position.x - moveIncrement, transform.position.y);
                transform.position = pos;

                randomPathObject();
            }
            direction = 5;
        }
        else if (direction == 5 || direction == 6) { // Move pathgenerator upwards
            if (transform.position.y < maxY) {
                Vector2 pos = new Vector2(transform.position.x, transform.position.y + moveIncrement);
                transform.position = pos;

                randomPathObject();

                direction = Random.Range(1, 7);
            }
            else { // finished generating
                stopGeneration = true;
                endPos = transform.position;
            }
        }
    }

    private void randomPathObject() {
        int randN = Random.Range(0, totalWeight + 1);

        int i = 0;
        while (randN >= 0 && i < paths.Length) {
            randN -=  weights[i];
            i++;
        }

        GameObject pathChild = Instantiate(paths[i-1], transform.position, Quaternion.identity);
        pathChildren.Add(pathChild);

        /*
        int randPathObject = Random.Range(0, paths.Length);
        GameObject pathChild = Instantiate(paths[randPathObject], transform.position, Quaternion.identity);
        pathChildren.Add(pathChild);*/
    }
}

