using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthKilled : MonoBehaviour
{
    EnemyHealth enemyHealth;
    AISimples enemy;
    Animator anim;
    StealthKill player;
    public bool canBeStealthKilled;

    public void Start()
    {
        enemyHealth = GetComponentInParent<EnemyHealth>();
        enemy = GetComponentInParent<AISimples>();
        anim = GetComponentInParent<Animator>();
        canBeStealthKilled = false;
    }

    public void Update()
    {
        if (canBeStealthKilled)
        {
            if (player.stealthKill)
            {
                Debug.Log("Killed");
                enemyHealth.health = 0;
                anim.SetTrigger("Kill");
                enemy.canBeStealthKilled = false;
                canBeStealthKilled = false;
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
        if (other.CompareTag("Player") && enemy.canBeStealthKilled)
        {
            canBeStealthKilled = false;
            player = null;
        }
    }
}
