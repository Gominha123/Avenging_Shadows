using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artefact : MonoBehaviour, IInteractable
{
    //[SerializeField] private string prompt;

    public Item item;

    private string prompt;

    public string InteractablePrompt => "Press E to Pick Up" + prompt;

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

    }

    IEnumerator DoAfterTenSeconds()
    {
        yield return new WaitForSeconds(10);

        prompt = item.name;

    }
}
