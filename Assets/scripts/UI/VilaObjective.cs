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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        objectiveTextUI = player.GetComponentInChildren<ObjectiveText>();
        objectiveTextUI.SetUp(objectiveText);
        GameObject spawn = GameObject.Find("SpawnPoint");
        Inventory.Instance.gameObject.transform.parent.transform.position = spawn.transform.position;
    }

    public void Update()
    {

        if (Inventory.Instance.FindById(item.id, false))
        {
            objectiveTextUI.SetUp("Go to the Ship and Leave");
        }
    }
}
