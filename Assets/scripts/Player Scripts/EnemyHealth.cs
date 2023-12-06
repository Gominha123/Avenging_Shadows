using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public bool isDead;
    Animator anim;
    FloatingHealthBar healthBar;
    
    
    public void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        anim = GetComponent<Animator>();
    }

    public void Start()
    {
        //isDead = false;
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar(health, maxHealth);
        //anim.SetBool("TakeDamage", true);
        anim.SetTrigger("TakeDamage");
        if (health <= 0)
        {
            //isDead = true;
            anim.SetTrigger("isDead");
        }
    }

}
