using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInteraction : MonoBehaviour, IInteractable
{
    private string prompt = "Press E to Grab the Map";
    //[SerializeField] private InventoryManager inventory;

    public Item item;
    public string InteractablePrompt => prompt;

    public void Interact()
    {
        //if(!inventory.hasLetter)
        //{
        //    Debug.Log("Grabed");
        //    inventory.hasLetter = true;
        //    prompt = null;
        //    Destroy(gameObject);
        //}

        Inventory.Instance.Add(item);
        Destroy(gameObject);
    }
}
