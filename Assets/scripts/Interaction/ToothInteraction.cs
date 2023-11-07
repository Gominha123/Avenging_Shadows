using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothInteraction : MonoBehaviour, IInteractable
{
    //[SerializeField] private string prompt;

    public Item item;

    private string prompt;


    //public IWeapon tooth;

    //private void Awake()
    //{
    //    tooth = new ToothDecorator(item.value);
    //}

    public string InteractablePrompt => "Press E to Pick Up";//+ prompt;

    public void Interact()
    {
        if (Inventory.Instance.Count() < 10)
        {
            Inventory.Instance.Add(item);
            //Destroy(gameObject);

            gameObject.SetActive(false);
            Inventory.Instance.AddObject(gameObject);
            //tooth = new ToothDecorator(damage);
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
