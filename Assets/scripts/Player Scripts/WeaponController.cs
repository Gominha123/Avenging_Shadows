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

public class WeaponController : MonoBehaviour, IWeapon, IInteractable
{
    public Weapon weaponItem;

    public IWeapon weapon { get; set; }
    public float attackCooldown;
    public float damage;
    public bool enableAttack;
    public int durability;

    private void Awake()
    {
        if(weaponItem.weaponType == WeaponType.tooth)
        {
            SetWeapon();
        }
        
    }

    public float UpdateDamage()
    {
        return weapon.UpdateDamage();
    }

    public void SetWeapon()
    {
        weapon = WeaponFactory.Create(weaponItem);
        damage = weaponItem.upgradeDamage;
        durability = weaponItem.durability;

    }

    public Weapon GetWeaponItem()
    {
        Weapon weaponItemCopy = new()
        {
            upgradeDamage = damage,
            durability = durability,
            weaponType = weaponItem.weaponType
        };

        return (weaponItemCopy);
    }

    public void Start()
    {
        enableAttack = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && enableAttack && weaponItem.weaponType == WeaponType.weapon)
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            //se tiver como comentário passar a codigo se não não dá damage
            enemy.TakeDamage(damage);
            durability--;
            if(durability <= 0) {
                StartCoroutine(DoAfterOneSeconds());
                //this.GetComponentInParent<WeaponSwitch>().DeleteEquipedOnDurability(this.transform);
            }
        }
    }

    public Item item;

    private string prompt;

    public string InteractablePrompt => "Press E to Pick Up" + prompt;

    public void Interact()
    {
        if (Inventory.Instance.weaponCount < 2 && weaponItem.weaponType != WeaponType.tooth)
        {
            Inventory.Instance.Add(item, weaponItem);

            Inventory.Instance.invWeaponDamage[Inventory.Instance.weaponCount - 1] = weaponItem.upgradeDamage;
            Inventory.Instance.invWeaponDurability[Inventory.Instance.weaponCount - 1] = weaponItem.durability;

            Destroy(gameObject);
        }
        else if(Inventory.Instance.upgradeCount < 3 && weaponItem.weaponType == WeaponType.tooth) {
            Inventory.Instance.Add(item);

            gameObject.SetActive(false);
            Inventory.Instance.AddObject(gameObject);
        }
        else
        {
            prompt = "Inventory is Full";
            StartCoroutine(DoAfterFiveSeconds());
        }
        

    }

    IEnumerator DoAfterFiveSeconds()
    {
        yield return new WaitForSeconds(5);

        prompt = item.name;

    }

    IEnumerator DoAfterOneSeconds()
    {
        yield return new WaitForSeconds(1);

        this.GetComponentInParent<WeaponSwitch>().DeleteEquipedOnDurability(this.transform);

    }
}
