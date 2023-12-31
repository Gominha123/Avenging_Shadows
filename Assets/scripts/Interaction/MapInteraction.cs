using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MapInteraction : MonoBehaviour, IInteractable
{
    private string prompt = "Press E to Grab the Map";
    //[SerializeField] private InventoryManager inventory;

    public Item item;
    public string InteractablePrompt => prompt;

    public InteractionPromptUI interactionPromptUI;

    PlayableDirector playableDirector;

    public GameObject playerPos;

    GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        interactionPromptUI = player.GetComponentInChildren<InteractionPromptUI>();
        playableDirector = GameObject.FindGameObjectWithTag("CutsceneDirector").GetComponent<PlayableDirector>();
    }
    public void Interact()
    {
        Inventory.Instance.Add(item);
        player.transform.position = playerPos.transform.position;
        playableDirector.Play();
        interactionPromptUI.Close();
    }
}
