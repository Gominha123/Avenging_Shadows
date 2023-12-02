using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artefact : MonoBehaviour, IInteractable
{
    //[SerializeField] private string prompt;

    public Item item;

    private string prompt;

    public string InteractablePrompt => "Press E to Pick Up" + prompt;

    public InteractionPromptUI interactionPromptUI;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        interactionPromptUI = player.GetComponentInChildren<InteractionPromptUI>();
    }

    public void Interact()
    {
        if (Inventory.Instance.artifactCount < 4)
        {
            Inventory.Instance.Add(item);
            Destroy(gameObject);
        }
        else
        {
            prompt = "Inventory is Full";
            StartCoroutine(DoAfterTenSeconds());
        }

        interactionPromptUI.Close();

    }

    IEnumerator DoAfterTenSeconds()
    {
        yield return new WaitForSeconds(10);

        prompt = item.name;

    }
}
