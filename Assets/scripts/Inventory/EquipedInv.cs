using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EquipedInv : MonoBehaviour
{
    public float tempCurrentWeaponDamage;
    public int tempCurrentWeaponDurability;
    public int tempCurrentWeaponUpgrade;
    public float tempOldWeaponDamage;
    public int tempOldWeaponDurability;
    public int tempOldWeaponUpgrade;

    public GameObject toothObject;

    public GameObject weaponHolder;
    public Item[] items = new Item[2];

    public Item toothItem;

    //public readonly Item[] nullItems = new Item[2];

    public Item tempItem = null;

    public UnityEngine.UI.Button weapon1;
    public UnityEngine.UI.Button weapon2;
    private Sprite[] sprites = new Sprite[2];

    public Sprite defaultSprite1, defaultSprite2;

    public OpenInventory openInv;

    public InvDescription invDescription;

    public UnityEngine.UI.Image currentWeaponIcon;

    public int button;

    public bool coroutine = false;

    public WeaponSwitch weaponSwitch;

    private void Awake()
    {
        sprites[0] = defaultSprite1;
        sprites[1] = defaultSprite2;
        //tempCurrentWeapon = new Weapon();
        //tempOldWeapon = new Weapon();
    }

    private void Start()
    {
        ShowEquiped();
        //nullItems[0].icon = defaultSprite1;
        //nullItems[1].icon = defaultSprite2;
    }

    //private void Update()
    //{
    //    Debug.Log(button);
    //}

    public void Add(Item item,  int index)
    {
        items[index] = item;
    }

    public void Remove()
    {
        items[button] = null;
        weaponSwitch.DeleteWeapon(button);
        invDescription.Close();
        ShowEquiped();
    }

    public void RemoveEquiped()
    {
        //items[button] = nullItems[button];
        if(weaponSwitch.transform.childCount == 1)
        {
            invDescription.SetUpForEquiped("", "You only have this weapon make sure you dont lose it", false);
            return;
        }
        else if(weaponSwitch.isScrollable)
        {
            items[button] = null;
        }
        weaponSwitch.DeleteEquiped(button);
        ShowEquiped();
    }

    public void ButtonClick1()                  // tirar o discard red blade e blue
    {
        button = 0;
        if (coroutine || items[0] == null) return;
        weaponSwitch.GetWeaponItem(button);
        if (items[0].id == 12 || items[0].id == 13)
        {
            invDescription.SetUpForEquiped(items[0].name, items[0].description + "\nDamage: " + tempOldWeaponDamage + " Durability: " + "Infinite" + " Upgradability: " + tempOldWeaponUpgrade + "/3", false);
        }
        else
        {
            invDescription.SetUpForEquiped(items[0].name, items[0].description + "\nDamage: " + tempOldWeaponDamage + " Durability: " + tempOldWeaponDurability + " Upgradability: " + tempOldWeaponUpgrade + "/3", true);
        }
        
    }

    public void ButtonClick2()
    {
        button = 1;
        if (coroutine || items[1] == null) return;
        weaponSwitch.GetWeaponItem(button);
        if (items[1].id == 12 || items[1].id == 13)
        {
            invDescription.SetUpForEquiped(items[0].name, items[0].description + "\nDamage: " + tempOldWeaponDamage + " Durability: " + "Infinite" + " Upgradability: " + tempOldWeaponUpgrade + "/3", false);
        }
        else
        {
            invDescription.SetUpForEquiped(items[0].name, items[0].description + "\nDamage: " + tempOldWeaponDamage + " Durability: " + tempOldWeaponDurability + " Upgradability: " + tempOldWeaponUpgrade + "/3", true);
        }
    }

    public void ShowEquiped()
    {
        int count = 1;
        foreach (Item item in items)
        {
            var weapon = transform.Find("Weapon" + count).GetComponent<UnityEngine.UI.Button>();
            var icon = weapon.transform.Find("EquipedIcon").GetComponent<UnityEngine.UI.Image>();
            if (item != null )//nullItems[0] && item != nullItems[1])
            {
                icon.sprite = item.icon;
                icon.color = new Color32 (255, 255, 255, 255);
            }
            else
            {
                icon.sprite = sprites[count - 1];//nullItems[count-1].icon;
                icon.color = new Color32(255, 255, 255, 50);
            }
            count++;
        }
    }

    public void ReturnButtonClick()
    {
        StartCoroutine(GetItemSelect());
        coroutine = true;
    }

    public void ChangeCurrentWeaponIcon(int index)
    {
        if (items[index] == null)//nullItems[0] || items[index] == nullItems[1])
        {
            currentWeaponIcon.sprite = sprites[index];
            currentWeaponIcon.color = new Color32(255, 255, 255, 50);
        }
        else
        {
            currentWeaponIcon.sprite = items[index].icon;
            currentWeaponIcon.color = new Color32(255, 255, 255, 255);
        }
    }

    //public void ChangeToNullIcon(int index)
    //{
    //    currentWeaponIcon.sprite = sprites[index];
    //}

    public void FirstWeaponEquip()
    {
        items[0] = tempItem;
        weaponSwitch.DeleteWeapon(0);
        weaponSwitch.AddWeapon(items[0].name, true, tempCurrentWeaponDamage, tempCurrentWeaponDurability, tempCurrentWeaponUpgrade);
        invDescription.Close();
        openInv.letDisable = true;
        ShowEquiped();
    }

    public void ReturnButtonClickForUpgrade()
    {
        StartCoroutine(GetItemSelectForUpgrade());
        coroutine = true;
    }
    ///                                              mudar AQUI PARA AS WEAPONS GUARDAREM UPGRADES
    IEnumerator GetItemSelect()
    {
        // ...
        var waitForButton = new WaitForUIButtons(weapon1, weapon2);
        yield return waitForButton.Reset();
        if (waitForButton.PressedButton == weapon1)
        {
            //Debug.Log("1");
            coroutine = false;
            if (items[0] != null)
            {
                weaponSwitch.GetWeaponItem(0);
                Inventory.Instance.Add(items[0]);
                Inventory.Instance.SetUpDamageDurability(tempOldWeaponDamage,tempOldWeaponDurability, tempOldWeaponUpgrade);
            }
            items[0] = tempItem;
            weaponSwitch.DeleteWeapon(0);
            weaponSwitch.AddWeapon(items[0].name, true, tempCurrentWeaponDamage, tempCurrentWeaponDurability, tempCurrentWeaponUpgrade);
            Inventory.Instance.ListItems();
            invDescription.Close();
            openInv.letDisable = true;
            ShowEquiped();
        }
        else
        {
            //Debug.Log("2");
            coroutine = false;
            if (items[1] != null)
            {
                weaponSwitch.GetWeaponItem(1);
                Inventory.Instance.Add(items[1]);
                Inventory.Instance.SetUpDamageDurability(tempOldWeaponDamage, tempOldWeaponDurability, tempOldWeaponUpgrade);
            }
            items[1] = tempItem;
            weaponSwitch.DeleteWeapon(1);
            weaponSwitch.AddWeapon(items[1].name, false, tempCurrentWeaponDamage, tempCurrentWeaponDurability, tempCurrentWeaponUpgrade);
            Inventory.Instance.ListItems();
            invDescription.Close();
            openInv.letDisable = true;
            ShowEquiped();
        }
        // ...
    }

    IEnumerator GetItemSelectForUpgrade()
    {
        // ...
        var waitForButton = new WaitForUIButtons(weapon1, weapon2);
        yield return waitForButton.Reset();
        if (waitForButton.PressedButton == weapon1)
        {
            //Debug.Log("1");
            coroutine = false;
            if (items[0] != null)
            {
                //tooth = WeaponFactory.Create(Tooth);
                invDescription.Close();
                weaponSwitch.UpgradeWeapon(0, toothObject, toothItem);
                Inventory.Instance.ListItems();
                openInv.letDisable = true;
                ShowEquiped();
            }
        }
        else
        {
            //Debug.Log("2");
            coroutine = false;
            if (items[1] != null)
            {
                //tooth = new ToothDecorator(tempItem.value);
                //tooth = WeaponFactory.Create(Tooth);
                weaponSwitch.UpgradeWeapon(1, toothObject, toothItem);


                Inventory.Instance.ListItems();
                invDescription.Close();
                openInv.letDisable = true;
                ShowEquiped();
            }
        }
        // ...
    }
}
