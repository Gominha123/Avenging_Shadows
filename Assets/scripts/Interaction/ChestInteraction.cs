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

    public InteractionPromptUI interactionPromptUI;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        interactionPromptUI = player.GetComponentInChildren<InteractionPromptUI>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Interact()
    {
        animator.SetBool("HasOpened", true);
        gameObject.layer = 0;
        StartCoroutine(DoAfterTwoSeconds());
        interactionPromptUI.Close();
    }

    public void SpawnRandomItem()
    {
        int random = Random.Range(0, 3);
        GameObject weaponPrefab;
        GameObject weapon;

        random = 1;

        switch (random)
        {
            case 0:
                weaponPrefab = (GameObject)Resources.Load("Weapons/Sword_OH");
                weapon = Instantiate(weaponPrefab);
                Vector3 x;
                spawnPoint.transform.localPosition += new Vector3(0.5F, 0, 0);
                weapon.transform.position = spawnPoint.transform.position;
                weapon.transform.rotation = spawnPoint.transform.rotation;
                weapon.transform.Rotate(new Vector3(0, -90f, 0));

                break;
            case 1:
                weaponPrefab = (GameObject)Resources.Load("Weapons/LongSword");
                weapon = Instantiate(weaponPrefab);
                weapon.transform.position = spawnPoint.transform.position;
                weapon.transform.rotation = spawnPoint.transform.rotation;
                weapon.transform.Rotate(new Vector3(0, 90f, 0));
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
