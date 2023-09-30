using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseInteraction : MonoBehaviour, IInteractable
{
    private string prompt = "Find the Letter Before Leaving";
    [SerializeField] private Inventory inventory;
    public string InteractablePrompt => prompt;

    public void Update()
    {
        if (inventory.hasLetter)
        {
            prompt = "Press E to Leave";
        }
    }

    public void Interact()
    {
        if (inventory.hasLetter)
        {
            Debug.Log("Leaving");
        }
    }
}
