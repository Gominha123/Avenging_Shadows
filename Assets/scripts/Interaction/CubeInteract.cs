using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;

    private string tempPrompt;

    public Item item;

    public string InteractablePrompt => tempPrompt;


    private void Start()
    {
        tempPrompt = prompt;
    }

    //private void Update()
    //{
    //    if(Inventory.Instance.Count() < 10)
    //    {
    //        tempPrompt = "Press E to" + prompt;
    //    }
    //    else
    //    {
    //        tempPrompt = "Inventory is Full";
    //    }
    //}

    public void Interact()
    {
        Debug.Log(tempPrompt + " " + Inventory.Instance.Count());
        if(Inventory.Instance.Count() < 10) {
            Inventory.Instance.Add(item);
            Destroy(gameObject);
        }
        else
        {
            tempPrompt = "Inventory is Full";
            StartCoroutine(DoAfterTenSeconds());
        }
        
    }

    IEnumerator DoAfterTenSeconds()
    {
        yield return new WaitForSeconds(10);

        tempPrompt = prompt;
        
    }
}
