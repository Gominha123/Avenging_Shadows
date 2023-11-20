using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapaDaNeve : MonoBehaviour
{
    [SerializeField] ObjectiveText objectiveTextUI;
    //[SerializeField] InventoryManager inventory;

    private string objectiveText = "Choose a Weapon";

    public NeveHorseInteraction horse;

    public Item item;
    public Item item2;

    public GameObject redBlade;
    public GameObject blueBlade;

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

        if (Inventory.Instance.FindById(item.id, true) || Inventory.Instance.FindById(item2.id, true))
        {
            objectiveTextUI.SetUp("Go Back to Your Horse and Leave");
            Destroy(redBlade);
            Destroy(blueBlade);
            horse.weapon = true;
        }
    }
}
