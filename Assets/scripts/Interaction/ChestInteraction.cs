using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteraction : MonoBehaviour, IInteractable
{
    private string prompt = "Press E to Open Chest";
    //[SerializeField] private InventoryManager inventory;

    public Item item;
    public Animator animator;
    public string InteractablePrompt => prompt;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        prompt = null;
        animator.SetBool("HasOpened", true);
    }
}
