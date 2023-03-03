using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIpositioner : MonoBehaviour
{

    public GameObject player;
    public GameObject[] bottles;
    private int offset = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject bottle in bottles)
        {
            offset += 3;
            bottle.transform.position = new Vector3(transform.position.x, player.transform.position.y+offset, transform.position.z);
        }
        
    }
}
