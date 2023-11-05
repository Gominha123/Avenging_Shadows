using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public bool isDead;
    Animator anim;

    public void Start()
    {
        //isDead = false;
        anim = GetComponent<Animator>();
    }



    public void TakeDamage(float damage)
    {
        health -= damage;
        //anim.SetBool("TakeDamage", true);
        anim.SetTrigger("TakeDamage");
        if (health <= 0)
        {
            //isDead = true;
            anim.SetTrigger("isDead");
        }
    }

}
