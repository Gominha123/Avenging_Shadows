using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasteloObjective : MonoBehaviour
{
    [SerializeField] ObjectiveText objectiveTextUI;
    //[SerializeField] InventoryManager inventory;

    private string objectiveText = "Find and Grab the Map";

    public Item item;

    public void Start()
    {
        objectiveTextUI.SetUp(objectiveText);
        GameObject spawn = GameObject.Find("SpawnPoint");
        Inventory.Instance.gameObject.transform.parent.transform.position = spawn.transform.position;
    }

    public void Update()
    {

        if (Inventory.Instance.FindById(item.id, false))
        {
            objectiveTextUI.SetUp("Go Back to Your Horse and Leave");
        }
    }
}
