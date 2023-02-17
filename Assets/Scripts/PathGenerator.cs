using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    public Transform[] startingPositions; // Possible starting positions for path generation
    public Transform[] extremePoints; // max min values for x and y
    private float minX, maxX, minY, maxY; 
    public GameObject[] paths; // Array of objects that can be spawned on the 'path'
    public GameObject[] obstacles; // Array of obstacles that can be spawned where there is no path

    private int direction;
    public bool stopGeneration;

    public float moveIncrement;
    private float timeBtwSpawn;
    public float startTimeBtwSpawn;

    public LayerMask whatIsPath; 

    public Vector3 startPos;

    private void Start()
    {
        // If there's no previous chunk, generate a random starting position, 
        // otherwise make use of the previous chunk's finishing position
        if (startPos == new Vector3(0,0,0)) {
            int randStartingPos = Random.Range(0, startingPositions.Length);
            transform.position = startingPositions[randStartingPos].position;
        } else {
            transform.position = startPos;
        }
        
        randomPathObject();

        // Random direction for the path generator
        direction = Random.Range(1, 7);

        minX = extremePoints[0].position.x;
        maxX = extremePoints[1].position.x;
        minY = extremePoints[0].position.y;
        maxY = extremePoints[1].position.y;
    }

    private void Update()
    {
        if (timeBtwSpawn <= 0 && stopGeneration == false)
        {
            Move();
            timeBtwSpawn = startTimeBtwSpawn;
        }
        else {
            timeBtwSpawn -= Time.deltaTime;
        }
    }

    private void Move()
    {

        if (direction == 1 || direction == 2)
        { // Move pathgenerator to the right
            if (transform.position.x < maxX)
            {
                Vector2 pos = new Vector2(transform.position.x + moveIncrement, transform.position.y);
                transform.position = pos;

                randomPathObject();

                // Makes sure the pathgenerator doesn't move left
                /*direction = Random.Range(1, 7);
                if (direction == 3)
                {
                    direction = 1;
                }
                else if (direction == 4)
                {
                    direction = 5;
                }*/
                direction = 5;
            }
            else {
                direction = 5;
            }
        }
        else if (direction == 3 || direction == 4)
        { // Move pathgenerator to the left
            if (transform.position.x > minX)
            {
                Vector2 pos = new Vector2(transform.position.x - moveIncrement, transform.position.y);
                transform.position = pos;

                randomPathObject();

                //direction = Random.Range(3, 7);
                direction = 5;
            }
            else {
                direction = 5;
            }
        }
        else if (direction == 5 || direction == 6)
        { // Move pathgenerator upwards
            if (transform.position.y < maxY)
            {
                Vector2 pos = new Vector2(transform.position.x, transform.position.y + moveIncrement);
                transform.position = pos;

                randomPathObject();

                direction = Random.Range(1, 7);
            }
            else {
                stopGeneration = true;
            }
        }
    }

    private void randomPathObject() {
        int randPathObject = Random.Range(0, paths.Length);
        Instantiate(paths[randPathObject], transform.position, Quaternion.identity);
    }
}

