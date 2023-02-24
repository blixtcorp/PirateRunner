using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyIncreaser : MonoBehaviour
{
    public List<GameObject> eligibleChildren;
    private GameObject pathGen;
    private bool hasRun = false;
    
    // Start is called before the first frame update
    void Start()
    {
        pathGen = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (pathGen.GetComponentInChildren<PathGenerator>().stopGeneration && !hasRun) {
            collectEligibleChildren();
            spawnObstacles();
            hasRun = true;
        }
    }

    private void collectEligibleChildren() {
        eligibleChildren.Clear(); // Reset the list
        
        GameObject positions = transform.GetChild(1).gameObject;

        foreach (Transform child in positions.transform)
        {
            // Check if child has a boolean component named "myBoolean" set to true
            if (child.GetComponentInChildren<SpawnObstacle>() != null && child.GetComponentInChildren<SpawnObstacle>().isEligible == true)
            {
                // Add child to list
                eligibleChildren.Add(child.gameObject);
            }
        }
        Debug.Log("Eligible children: " + eligibleChildren.Count);
    }

    private void spawnObstacles() {
        int amountOfObstacles = DifficultyLevel();

        if (amountOfObstacles > eligibleChildren.Count) {
            amountOfObstacles = eligibleChildren.Count;
        }

        for (int i = 0; i < amountOfObstacles; i++) {
            int rand = Random.Range(0, eligibleChildren.Count);
            eligibleChildren[rand].GetComponentInChildren<SpawnObstacle>().instantiateObstacle();
            eligibleChildren.RemoveAt(rand);
        }
    }

    private int DifficultyLevel() {
        GameObject player = GameObject.Find("Player");
        int difLev = 0;

        // Check if the player object has the specific component
        if (player.transform != null)
        {
            float distanceTraveled = player.transform.position.y;
            if (distanceTraveled < 100f) {
                difLev = 1;
            }
            else {
                difLev = (int) (distanceTraveled / 100f);
            }
            Debug.Log("distance traveled: " + distanceTraveled + ". Difficulty level: " + difLev);
        }
        

        return difLev;
    }
}
