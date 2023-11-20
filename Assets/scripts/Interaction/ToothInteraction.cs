using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothInteraction : MonoBehaviour, IInteractable
{
    //[SerializeField] private string prompt;

    InteractionPromptUI promptUI;

    public Item item;

    private string prompt;

    private string tempPrompt;


    //public IWeapon tooth;

    //private void Awake()
    //{
    //    tooth = new ToothDecorator(item.value);
    //}

    private void Start()
    {
        promptUI = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<InteractionPromptUI>();
        tempPrompt = "Press E to Pick Up Tooth";
        prompt = tempPrompt;
    }

    public string InteractablePrompt => prompt;

    public void Interact()
    {
        if (Inventory.Instance.upgradeCount < 3)
        {
            Inventory.Instance.Add(item);
            //Destroy(gameObject);

            gameObject.SetActive(false);
            Inventory.Instance.AddObject(gameObject);
            //tooth = new ToothDecorator(damage);
        }
        else
        {
            promptUI.Close();
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
