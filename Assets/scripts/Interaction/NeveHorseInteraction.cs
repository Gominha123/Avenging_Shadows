using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NeveHorseInteraction : MonoBehaviour, IInteractable
{
    public string prompt = "Choose a Weapon Before Leaving";
    //[SerializeField] private InventoryManager inventory;

    public bool weapon = false;
    public string InteractablePrompt => prompt;
    public string scene;

    public GameObject loadCanvas;
    public GameObject slider;

    public void Update()
    {
        if (weapon)
        {
            prompt = "Press E to Leave";
        }
    }

    public void Interact()
    {
        if (weapon)
        {
            loadCanvas.SetActive(true);
            slider.SetActive(true);
            StartCoroutine(LoadAsync());
        }
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation loadOperaton = SceneManager.LoadSceneAsync(scene);
        while (!loadOperaton.isDone)
        {
            yield return null;
        }
    }
}
