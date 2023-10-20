using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    //public GameObject Weapon;
    public float attackCooldown;
    public float damage;
    public bool enableAttack;

    public void Start()
    {
        enableAttack = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy"  && enableAttack)
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            enemy.TakeDamage(damage);
        }
    }

}
