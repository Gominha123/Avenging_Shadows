using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthKilled : MonoBehaviour
{
    EnemyHealth enemyHealth;
    AISimples enemy;
    Animator anim;
    StealthKill player;
    BoxCollider boxCollider;

    public bool canBeStealthKilled;
    InteractionPromptUI interactionPromptUI;


    public void Start()
    {
        enemyHealth = GetComponentInParent<EnemyHealth>();
        enemy = GetComponentInParent<AISimples>();
        anim = GetComponentInParent<Animator>();
        boxCollider = GetComponent<BoxCollider>();

        GameObject player = GameObject.FindWithTag("Player");

        if(player != null )
        {
            interactionPromptUI = player.GetComponentInChildren<InteractionPromptUI>();
        }

        canBeStealthKilled = false;
    }

    public void Update()
    {
        if (canBeStealthKilled)
        {
            interactionPromptUI.SetUp("Press F to Assassinate");
            if (player.stealthKill)
            {
                enemyHealth.health = 0;
                anim.SetTrigger("Kill");
                enemy.canBeStealthKilled = false;
                canBeStealthKilled = false;
                boxCollider.enabled = false;
                player.stealthKill = false;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && enemy.canBeStealthKilled)
        {
            canBeStealthKilled = true;
            player = other.GetComponent<StealthKill>();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canBeStealthKilled = false;
            player = null;
        }
    }
}
