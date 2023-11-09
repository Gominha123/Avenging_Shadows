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
    [SerializeField] Weapon weaponItem;
    public IWeapon weapon { get; set; }
    public float attackCooldown;
    public float damage;
    public bool enableAttack;
    public int durability;

    private void Awake()
    {
        weapon = WeaponFactory.Create(weaponItem);
        damage = weaponItem.upgradeDamage;
        durability = weaponItem.durability;
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
        if (other.tag == "Enemy" && enableAttack && weaponItem.weaponType == WeaponType.weapon)
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            enemy.TakeDamage(damage);
            durability--;
            if(durability <= 0) {
                this.GetComponentInParent<WeaponSwitch>().DeleteEquipedOnDurability(this.transform);
            }
        }
    }

    public Item item;

    private string prompt;

    public string InteractablePrompt => "Press E to Pick Up" + prompt;

    public void Interact()
    {
        if (Inventory.Instance.Count() < 10)
        {
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
}
