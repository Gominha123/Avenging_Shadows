using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;

    public string InteractablePrompt => "Press E to " + prompt;

    public void Interact()
    {
        Debug.Log("Open");
    }
}
