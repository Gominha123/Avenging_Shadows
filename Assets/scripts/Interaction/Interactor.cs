using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

interface IInteractable
{
    public void Interact();
    public string InteractablePrompt { get; }
}
public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactPoint;
    [SerializeField] private float interactRadius = 0.5f;
    [SerializeField] private LayerMask interactibleMask;
    [SerializeField] private int numFound;
    [SerializeField] private InteractionPromptUI interactPromptUI;

    private Collider[] colliders;

    private IInteractable interactable;

    void Awake()
    {
        colliders = new Collider[GameObject.FindGameObjectsWithTag("Interactable").Length];
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        numFound = Physics.OverlapSphereNonAlloc(interactPoint.position, interactRadius, colliders, interactibleMask);

        if (numFound > 0)
        {
            interactable = colliders[0].GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (!interactPromptUI.isDisplayed) interactPromptUI.SetUp(interactable.InteractablePrompt);
                if (Input.GetKeyDown(KeyCode.E)) interactable.Interact();
            }
        }
        else
        {
            if (interactable != null) interactable = null;
            if (interactPromptUI.isDisplayed) interactPromptUI.Close();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactPoint.position, interactRadius);
    }
}
