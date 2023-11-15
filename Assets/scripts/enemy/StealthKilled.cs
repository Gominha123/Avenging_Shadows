using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthKilled : MonoBehaviour
{
    Transform player;
    public Animator anim;
    public Transform killPosition;

    public void SetParent()
    {
        anim.SetBool("Kill", true);
    }

    public void UnSetParent()
    {
        anim.SetBool("Kill", false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<StealthKill>()._enemy = this;
            other.GetComponent<StealthKill>()._killPosition = killPosition;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
        }
    }
}
