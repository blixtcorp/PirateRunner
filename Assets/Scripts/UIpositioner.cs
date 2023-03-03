using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIpositioner : MonoBehaviour
{

    public GameObject player;
    public GameObject[] bottles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject bottle in bottles)
        {
            int offset = 10;
            bottle.transform.position = new Vector3(transform.position.x-offset, player.transform.position.y, transform.position.z);
        }
        
    }
}
