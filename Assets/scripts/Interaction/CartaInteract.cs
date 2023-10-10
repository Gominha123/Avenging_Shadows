using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CartaInteract : MonoBehaviour, IInteractable
{
    private string prompt = "Press E to Grab the Letter";
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

        inventory.Instance.Add(item);
        Destroy(gameObject);
    }
}
