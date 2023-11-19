using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;

    InteractionPromptUI promptUI;

    private string tempPrompt;

    public Item item;

    public Weapon weapon;

    public string InteractablePrompt => tempPrompt;


    private void Start()
    {
        promptUI = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<InteractionPromptUI>();
        tempPrompt = "Press E to " + prompt;
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
        if(Inventory.Instance.weaponCount < 2) {
            Inventory.Instance.Add(item);
            Inventory.Instance.invWeaponDamage[Inventory.Instance.weaponCount - 1] = weapon.upgradeDamage;
            Inventory.Instance.invWeaponDurability[Inventory.Instance.weaponCount - 1] = weapon.durability;
            Destroy(gameObject);
        }
        else
        {
            promptUI.Close();
            tempPrompt = "Inventory is Full";
            StartCoroutine(DoAfterFiveSeconds());
        }
        
    }

    IEnumerator DoAfterFiveSeconds()
    {
        yield return new WaitForSeconds(5);

        tempPrompt = "Press E to " + prompt;
        
    }
}
