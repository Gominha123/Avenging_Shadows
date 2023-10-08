using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;

    public Item item;

    public string InteractablePrompt => "Press E to " + prompt;

    public void Interact()
    {
        inventory.Instance.Add(item);
        Destroy(gameObject);
    }
}
