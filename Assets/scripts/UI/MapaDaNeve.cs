using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapaDaNeve : MonoBehaviour
{
    [SerializeField] ObjectiveText objectiveTextUI;
    //[SerializeField] InventoryManager inventory;

    private string objectiveText = "Choose a Weapon";

    public Item item;
    public Item item2;

    public GameObject redBlade;
    public GameObject blueBlade;

    public void Start()
    {
        objectiveTextUI.SetUp(objectiveText);
    }

    public void Update()
    {

        if (Inventory.Instance.FindById(item.id, true) || Inventory.Instance.FindById(item2.id, true))
        {
            objectiveTextUI.SetUp("Go Back to Your Horse and Leave");
        }
    }
}
