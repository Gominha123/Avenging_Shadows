using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject settingsPanel;
    public GameObject deathPanel;

    private PlayerHealth playerHealth;
    private bool isPaused;
    private bool inSettings;

    // Start is called before the first frame update
    void Start()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(false);
        deathPanel.SetActive(false);
        playerHealth = GetComponentInParent<PlayerHealth>();

        inSettings = false;
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.isDead) Death();

        if (Input.GetKeyDown(KeyCode.Escape) && !playerHealth.isDead)
        {
            if (inSettings)
                Settings();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        if (pausePanel.activeSelf)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
            UnityEngine.Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    public void Settings()
    {
        inSettings = !inSettings;
        settingsPanel.SetActive(inSettings);
    }

    public void Death()
    {
        deathPanel.SetActive(true);
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void Respawn()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerHealth.Revive();
        deathPanel.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 0.0f;
    }

}
