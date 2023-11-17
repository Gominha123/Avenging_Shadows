using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VilaObjective : MonoBehaviour
{
    [SerializeField] ObjectiveText objectiveTextUI;
    //[SerializeField] InventoryManager inventory;

    private string objectiveText = "Find and Grab the Plans";

    public Item item;

    public void Start()
    {
        objectiveTextUI.SetUp(objectiveText);
    }

    public void Update()
    {

        if (Inventory.Instance.FindById(item.id, false))
        {
            objectiveTextUI.SetUp("Go to the Ship and Leave");
        }
    }
}
