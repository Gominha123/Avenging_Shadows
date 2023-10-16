using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    //public GameObject Weapon;
    public float attackCooldown;
    public float damage;
    public bool isAttacking;

    public void Start()
    {
        isAttacking = false;
    }

    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (!isAttacking)
            {
                Attack();
            }
        }
    }

    public void Attack()
    {
        isAttacking = true;
        StartCoroutine(ResetAttackCooldown());

    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy"  && isAttacking)
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            enemy.TakeDamage(damage);
        }
    }

}
