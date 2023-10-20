using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;

    public Item item;

    private string tempPrompt;

    public string InteractablePrompt => "Press E to " + prompt;

    private void Start()
    {
        tempPrompt = prompt;
    }

    public void Interact()
    {
        if (Inventory.Instance.Count() < 10)
        {
            Inventory.Instance.Add(item);
            Destroy(gameObject);
        }
        else
        {
            prompt = "Inventory is Full";
            StartCoroutine(DoAfterTenSeconds());
        }

    }

    IEnumerator DoAfterTenSeconds()
    {
        yield return new WaitForSeconds(10);

        prompt = tempPrompt;

    }
}
