using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IWeapon
{

    public float UpdateDamage();
    
}

public class CurrentWeapon : IWeapon
{
    readonly float damage;

    //public float damage;
    public CurrentWeapon(float damage)
    {
        this.damage = damage;
    }

    public float UpdateDamage()
    {
        return damage;
    }
}

public class WeaponController : MonoBehaviour, IWeapon
{
    [SerializeField] Weapon weaponItem;
    public IWeapon weapon { get; set; }
    public float attackCooldown;
    public float damage;
    public bool enableAttack;

    private void Awake()
    {
        weapon = WeaponFactory.Create(weaponItem);
    }

    public float UpdateDamage()
    {
        return weapon.UpdateDamage();
    }


    public void Start()
    {
        enableAttack = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && enableAttack && weaponItem.weaponType != WeaponType.tooth)
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            enemy.TakeDamage(damage);
        }
    }
}

//public class WeaponController : MonoBehaviour
//{
//    //public GameObject Weapon;
//    public float attackCooldown;
//    public float damage;
//    public bool enableAttack;

//    public void Start()
//    {
//        enableAttack = false;
//    }

//    public void OnTriggerEnter(Collider other)
//    {
//        if(other.tag == "Enemy" && enableAttack)
//        {
//            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
//            enemy.TakeDamage(damage);
//        }
//    }
//}
