using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float movementSpeed;
    private Rigidbody rb;
    private Vector2 movementDirection;

    [SerializeField] public float rotationSpeed;

    public float score;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        movementDirection = new Vector2(horizontalInput, verticalInput + 1);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        //movementDirection.Normalize();
        transform.Translate(movementDirection * movementSpeed * inputMagnitude * Time.deltaTime, Space.World);

        if (movementDirection != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TrashBlue")
        {
            score += 1;
            ScoreHandler.increaseScore(1);
            Destroy(other.gameObject);
            Debug.Log(score);
        }
        else if (other.tag == "TrashGreen")
        {
            score += 3;
            ScoreHandler.increaseScore(3);
            Destroy(other.gameObject);
            Debug.Log(score);
        }
        else if (other.tag == "TrashPurple")
        {
            score += 5;
            ScoreHandler.increaseScore(5);
            Destroy(other.gameObject);
            Debug.Log(score);
        }
        else if (other.tag == "Island"){
            SceneManager.LoadScene(1);
            PlayerPrefs.SetFloat("score", score);
        }
        
        Debug.Log(score);

    }
}
