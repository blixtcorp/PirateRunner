using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class NewGame : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler

{
    public GameObject button;
    public GameObject canvas;
    public GameObject pauseMenuCanvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(true);
        //pauseMenuCanvas.SetActive(false);

        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void click()
    {
        canvas.SetActive(false);
        Time.timeScale = 1;
        Debug.Log("clicked play");
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
