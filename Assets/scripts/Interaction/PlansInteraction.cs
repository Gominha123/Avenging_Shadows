using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlansInteraction : MonoBehaviour, IInteractable
{
    private string prompt = "Press E to Grab the Plans";
    //[SerializeField] private InventoryManager inventory;

    public Item item;
    public string InteractablePrompt => prompt;

    public InteractionPromptUI interactionPromptUI;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        interactionPromptUI = player.GetComponentInChildren<InteractionPromptUI>();
    }

    public void Interact()
    {

        Inventory.Instance.Add(item);
        Destroy(gameObject);
        interactionPromptUI.Close();
    }
}
