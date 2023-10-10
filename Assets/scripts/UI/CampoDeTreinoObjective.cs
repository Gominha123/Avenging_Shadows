using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampoDeTreinoObjective : MonoBehaviour
{
    [SerializeField] ObjectiveText objectiveTextUI;
    //[SerializeField] InventoryManager inventory;

    private string objectiveText = "Find and Grab the King´s Letter";

    public Item item;

    public void Start()
    {
        objectiveTextUI.SetUp(objectiveText);
    }

    public void Update()   
    {

        if (inventory.Instance.FindById(item.id))
        {
            objectiveTextUI.SetUp("Go Back to Your Horse and Leave");
        }
        //if (inventory.hasLetter)
        //{
        //    objectiveTextUI.SetUp("Go Back to Your Horse and Leave");
        //}
    }

}
