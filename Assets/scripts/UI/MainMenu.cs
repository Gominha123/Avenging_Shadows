using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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
