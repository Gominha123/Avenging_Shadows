using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public AudioMixer audioMixer;

    private void Start()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        audioMixer.SetFloat("Volume", -40F);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }

    public void SettingsMenu()
    {
        settingsMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
