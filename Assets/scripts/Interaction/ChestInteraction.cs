using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteraction : MonoBehaviour, IInteractable
{
    private string prompt = "Press E to Open Chest";
    //[SerializeField] private InventoryManager inventory;

    public Item item;
    public Animator animator;
    public GameObject spawnPoint;
    public string InteractablePrompt => prompt;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        gameObject.layer = 0;
        animator.SetBool("HasOpened", true);
        StartCoroutine(DoAfterTwoSeconds());
    }

    public void SpawnRandomItem()
    {
        int random = Random.Range(0, 2);
        GameObject weaponPrefab;
        GameObject weapon;

        switch (random)
        {
            case 0:
                weaponPrefab = (GameObject)Resources.Load("Weapons/Sword_OH Variant");
                weapon = Instantiate(weaponPrefab);
                weapon.transform.position = spawnPoint.transform.position;
                weapon.transform.rotation = Quaternion.Euler(90f, 90f, 0f);
                break;
            case 1:
                weaponPrefab = (GameObject)Resources.Load("Weapons/LongSword");
                weapon = Instantiate(weaponPrefab);
                weapon.transform.position = spawnPoint.transform.position;
                weapon.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                break;
            case 2:
                weaponPrefab = (GameObject)Resources.Load("Health Potion");
                weapon = Instantiate(weaponPrefab);
                weapon.transform.position = spawnPoint.transform.position;
                weapon.transform.rotation = spawnPoint.transform.rotation;
                break;
        }
    }

    IEnumerator DoAfterTwoSeconds()
    {
        yield return new WaitForSeconds(2);

        SpawnRandomItem();

    }
}
