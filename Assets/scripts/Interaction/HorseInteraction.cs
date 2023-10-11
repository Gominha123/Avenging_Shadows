using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseInteraction : MonoBehaviour, IInteractable
{
    private string prompt = "Find the Letter Before Leaving";
    //[SerializeField] private InventoryManager inventory;

    public Item item;
    public string InteractablePrompt => prompt;

    public void Update()
    {
        if (Inventory.Instance.FindById(item.id))
        {
            prompt = "Press E to Leave";
        }
    }

    public void Interact()
    {
        if (Inventory.Instance.FindById(item.id))
        {
            Debug.Log("Leaving");
        }
    }
}
