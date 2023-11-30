using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteraction : MonoBehaviour, IInteractable
{
    public string prompt = "Press E to Open Chest";
    //[SerializeField] private InventoryManager inventory;

    public Item item;
    Animator animator;
    public GameObject spawnPoint;
    public string InteractablePrompt => prompt;

    private void Start()
    {
        animator = GetComponent<Animator>();
        Debug.Log(animator);
    }

    public void Interact()
    {
        Debug.Log("here");
        Debug.Log("here");
        animator.SetBool("HasOpened", true);
        gameObject.layer = 0;
        StartCoroutine(DoAfterTwoSeconds());
    }

    public void SpawnRandomItem()
    {
        int random = Random.Range(0, 3);
        GameObject weaponPrefab;
        GameObject weapon;

        switch (random)
        {
            case 0:
                weaponPrefab = (GameObject)Resources.Load("Weapons/Sword_OH");
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
