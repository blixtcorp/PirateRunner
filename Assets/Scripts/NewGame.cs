using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement; 

public class NewGame : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler

{
    public GameObject button;
    public GameObject canvas;
    public GameObject pauseMenuCanvas;
    private bool gameIsPaused;

    // Start is called before the first frame update
    void Start()
    {
        gameIsPaused = false;
        canvas.SetActive(true);
        //pauseMenuCanvas.SetActive(false);

        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void click_play()
    {
        canvas.SetActive(false);
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1;
        Debug.Log("clicked play");
        SceneManager.LoadScene(2);
    }

    public void click_exit()
    {
        Debug.Log("exit game..");
        Application.Quit();
    }

    private void pause()
    {
        Debug.Log("pause game..");
        Time.timeScale = 0;
        pauseMenuCanvas.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().fontSize = 50;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().fontSize = 42;
    }

}
