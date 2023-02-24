using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class GameOverController : MonoBehaviour
{

    [SerializeField] public Text score;
    // Start is called before the first frame update
    void Start()
    {
        score.text = "Score: " + PlayerPrefs.GetFloat("score").ToString();

    }

    public void RestartButton(){
        SceneManager.LoadScene(2);
    }

    public void MainMenuButton(){
        SceneManager.LoadScene(3);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
