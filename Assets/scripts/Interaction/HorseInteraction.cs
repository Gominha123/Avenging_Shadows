using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HorseInteraction : MonoBehaviour, IInteractable
{
    public string prompt = "Find the Letter Before Leaving";
    //[SerializeField] private InventoryManager inventory;

    public Item item;
    public string InteractablePrompt => prompt;
    public string scene;


    public GameObject loadCanvas;
    public GameObject slider;

    private void Awake()
    {
        loadCanvas.SetActive(false);
    }

    public void Update()
    {
        if (Inventory.Instance.FindById(item.id, false))
        {
            prompt = "Press E to Leave";
        }
    }

    public void Interact()
    {
        if (Inventory.Instance.FindById(item.id, false))
        {
            loadCanvas.SetActive(true);
            slider.SetActive(true);
            StartCoroutine(LoadAsync());
        }
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation loadOperaton = SceneManager.LoadSceneAsync(scene);
        while(!loadOperaton.isDone)
        {
            yield return null;
        }
    }
}
