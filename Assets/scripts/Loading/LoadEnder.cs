using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadEnder : MonoBehaviour
{
    public GameObject loadCanvas;

    public GameObject panel;

    public GameObject slider;

    private void Awake()
    {
        loadCanvas.SetActive(true);
        panel.SetActive(true);
        slider.SetActive(false);
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            loadCanvas.SetActive(false);
            Time.timeScale = 1f;
            Destroy(gameObject);
        }
    }
}
