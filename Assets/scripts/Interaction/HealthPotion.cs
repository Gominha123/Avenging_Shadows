using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;

    InteractionPromptUI promptUI;

    public Item item;

    private string tempPrompt;

    public string InteractablePrompt => tempPrompt;

    private void Start()
    {
        promptUI = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<InteractionPromptUI>();
        tempPrompt = "Press E to " + prompt;
    }

    public void Interact()
    {
        if (Inventory.Instance.potionCount < 3)
        {
            Inventory.Instance.Add(item);
            Destroy(gameObject);
        }
        else
        {
            promptUI.Close();
            tempPrompt = "Inventory is Full";
            StartCoroutine(DoAfterTenSeconds());
        }

    }

    IEnumerator DoAfterTenSeconds()
    {
        yield return new WaitForSeconds(5);

        prompt = tempPrompt;

    }
}
