using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;

    public Item item;

    public string InteractablePrompt => "Press E to " + prompt;

    public void Interact()
    {
        Inventory.Instance.Add(item);
        Destroy(gameObject);
    }
}
