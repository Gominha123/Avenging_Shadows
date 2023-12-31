using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlansInteraction : MonoBehaviour, IInteractable
{
    private string prompt = "Press E to Grab the Plans";
    //[SerializeField] private InventoryManager inventory;

    public Item item;
    public string InteractablePrompt => prompt;

    public InteractionPromptUI interactionPromptUI;

    PlayableDirector playableDirector;

    public GameObject playerPos;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        interactionPromptUI = player.GetComponentInChildren<InteractionPromptUI>();
        playableDirector = GameObject.FindGameObjectWithTag("CutsceneDirector").GetComponent<PlayableDirector>();
    }

    public void Interact()
    {

        Inventory.Instance.Add(item);
        playerPos.transform.position = playerPos.transform.position;
        playableDirector.Play();
        //Destroy(gameObject);
        interactionPromptUI.Close();
    }
}
