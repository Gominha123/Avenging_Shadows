using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject SettingsPanel;
    // Start is called before the first frame update
    void Start()
    {
        SettingsPanel.SetActive(false);
        PausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if(PausePanel.activeSelf) { 
            PausePanel.SetActive(false);
            Time.timeScale = 1;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            PausePanel.SetActive(true);
            Time.timeScale = 0;
            UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void Settings()
    {
        SettingsPanel.SetActive(true);
    }

    public void MainMenu()
    {

    }


}
