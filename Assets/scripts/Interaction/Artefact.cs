using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artefact : MonoBehaviour, IInteractable
{
    //[SerializeField] private string prompt;

    public Item item;

    public string InteractablePrompt => "Press E to Pick Up" + item.name;

    public void Interact()
    {
        Inventory.Instance.Add(item);
        Destroy(gameObject);
    }
}
