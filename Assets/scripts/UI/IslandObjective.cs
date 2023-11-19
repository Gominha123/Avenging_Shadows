using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandObjective : MonoBehaviour
{
    [SerializeField] ObjectiveText objectiveTextUI;
    //[SerializeField] InventoryManager inventory;

    private string objectiveText = "Kill the Captain";

    public void Start()
    {
        objectiveTextUI.SetUp(objectiveText);
        GameObject spawn = GameObject.Find("SpawnPoint");
        Inventory.Instance.gameObject.transform.parent.transform.position = spawn.transform.position;
    }

}
