using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlansInteraction : MonoBehaviour
{
    private string prompt = "Press E to Grab the Plans";
    //[SerializeField] private InventoryManager inventory;

    public Item item;
    public string InteractablePrompt => prompt;

    public void Interact()
    {

        Inventory.Instance.Add(item);
        Destroy(gameObject);
    }
}
