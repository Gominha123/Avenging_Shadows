using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthKill : MonoBehaviour
{
    //public StealthKilled _enemy;
    public Animator anim;
    public bool stealthKill;

    public void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Stealth"))
        {
            StealthKilled _enemy = other.GetComponentInChildren<StealthKilled>();
            if (Input.GetKeyDown(KeyCode.F) && _enemy.canBeStealthKilled)
            {
                stealthKill = true;
                anim.SetTrigger("Kill");
            }
        }
    }

}
