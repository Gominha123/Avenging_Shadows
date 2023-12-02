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
        Debug.Log(animator);
    }
    private void Update()
    {
        Debug.Log(animator.GetBool("HasOpened"));
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

        random = 0;

        switch (random)
        {
            case 0:
                weaponPrefab = (GameObject)Resources.Load("Weapons/Sword_OH");
                weapon = Instantiate(weaponPrefab);
                Vector3 x;
                x = spawnPoint.transform.position;
                x.x = 442F;
                weapon.transform.position = x;
                weapon.transform.rotation = Quaternion.Euler(180f, 90f, 0f);
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
