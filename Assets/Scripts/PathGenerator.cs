using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    public Transform[] startingPositions;
    public Transform[] extremePoints; // max min values for x and y
    private float minX, maxX, minY, maxY;
    public GameObject[] paths;

    private int direction;
    private bool stopGeneration;

    public float moveIncrement;
    private float timeBtwSpawn;
    public float startTimeBtwSpawn;

    public LayerMask whatIsPath;

    private void Start()
    {
        // Change to boat position in future
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        randomPathObject();

        // Is this necessary?
        direction = Random.Range(1, 6);

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
        { // Move right !
            // change this to account for whatever
            if (transform.position.x < maxX)
            {
                Vector2 pos = new Vector2(transform.position.x + moveIncrement, transform.position.y);
                transform.position = pos;

                randomPathObject();

                // Makes sure the level generator doesn't move left !
                direction = Random.Range(1, 6);
                if (direction == 3)
                {
                    direction = 1;
                }
                else if (direction == 4)
                {
                    direction = 5;
                }
            }
            else {
                direction = 5;
            }
        }
        else if (direction == 3 || direction == 4)
        { // Move left !
           // Check this position later
            if (transform.position.x > minX)
            {
                Vector2 pos = new Vector2(transform.position.x - moveIncrement, transform.position.y);
                transform.position = pos;

                randomPathObject();

                direction = Random.Range(3, 6);
            }
            else {
                direction = 5;
            }
        }
        else if (direction == 5)
        { // MoveDown
        // double check position later
            if (transform.position.y > minY)
            {
                Vector2 pos = new Vector2(transform.position.x, transform.position.y - moveIncrement);
                transform.position = pos;

                randomPathObject();

                direction = Random.Range(1, 6);
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
