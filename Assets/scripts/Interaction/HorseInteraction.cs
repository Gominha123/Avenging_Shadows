using GLTF.Schema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HorseInteraction : MonoBehaviour, IInteractable
{
    public string prompt = "Find the Letter Before Leaving";
    //[SerializeField] private InventoryManager inventory;

    public Item item;
    private bool weapon;
    public string InteractablePrompt => prompt;
    public string scene;

    private void Awake()
    {
        if(item.itemType == Item.ItemType.Weapon)
        {
            weapon = true;
        }
        else { weapon = false; }
    }

    public void Update()
    {
        if (Inventory.Instance.FindById(item.id, weapon))
        {
            prompt = "Press E to Leave";
        }
    }

    public void Interact()
    {
        if (Inventory.Instance.FindById(item.id, weapon))
        {
            SceneManager.LoadScene(scene);
        }
    }
}
