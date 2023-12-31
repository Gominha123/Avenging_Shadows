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
    public int upCounter = 0;
    private InteractionPromptUI interactPromptUI;


    public string prompt;

    private string tempPrompt;

    private void Awake()
    {
        if(weaponItem.weaponType == WeaponType.tooth)
        {
            SetWeapon();
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        interactPromptUI = player.GetComponentInChildren<InteractionPromptUI>();
        
    }
    public float UpdateDamage()
    {
        if(upCounter <= 3)
        {
            upCounter++;
            Debug.Log(weapon);
            return weapon.UpdateDamage();
        }
        return damage;
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
        tempPrompt = "Press E to Pick Up " + prompt;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && enableAttack && weaponItem.weaponType == WeaponType.weapon)
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            //se tiver como coment�rio passar a codigo se n�o n�o d� damage
            enemy.TakeDamage(damage);
            durability--;
            TeethDropRate(enemy);
            if(durability == 0) {
                StartCoroutine(DoAfterOneSeconds());
            }
        }
    }

    public void TeethDropRate(EnemyHealth enemy)
    {
        int rangeValue = 40;
        int dropValue = Random.Range(1, 100);
        if(dropValue >= rangeValue) 
        {
            GameObject toothPrefab = (GameObject)Resources.Load("Weapons/Molar_Tooth");
            GameObject tooth = Instantiate(toothPrefab);
            tooth.transform.position = enemy.gameObject.transform.position;
        }
    }

    public Item item;

    public string InteractablePrompt => tempPrompt;

    public void Interact()
    {
        if (Inventory.Instance.weaponCount < 2 && weaponItem.weaponType != WeaponType.tooth)
        {
            Inventory.Instance.Add(item, weaponItem);

            Inventory.Instance.invWeaponDamage[Inventory.Instance.weaponCount - 1] = weaponItem.upgradeDamage;
            Inventory.Instance.invWeaponDurability[Inventory.Instance.weaponCount - 1] = weaponItem.durability;

            Destroy(gameObject);
            interactPromptUI.Close();
        }
        else if(Inventory.Instance.upgradeCount < 3 && weaponItem.weaponType == WeaponType.tooth) {
            Inventory.Instance.Add(item);

            gameObject.SetActive(false);
            Inventory.Instance.AddObject(gameObject);
            interactPromptUI.Close();
        }
        else
        {
            interactPromptUI.Close();
            tempPrompt = "Inventory is Full";
            StartCoroutine(DoAfterFiveSeconds());
        }
        

    }

    IEnumerator DoAfterFiveSeconds()
    {
        yield return new WaitForSeconds(5);

        tempPrompt = "Press E to Pick Up " + prompt;

    }

    IEnumerator DoAfterOneSeconds()
    {
        yield return new WaitForSeconds(1);

        this.GetComponentInParent<WeaponSwitch>().DeleteEquipedOnDurability(this.transform);

    }
}
