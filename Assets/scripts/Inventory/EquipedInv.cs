using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class EquipedInv : MonoBehaviour
{
    public GameObject weaponHolder;
    public Item[] items = new Item[2];

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
        items[button] = null;
        weaponSwitch.DeleteEquiped(button);
        invDescription.Close();
        ShowEquiped();
    }

    public void ButtonClick1()
    {
        button = 0;
        if (coroutine || items[0] == null) return;
        invDescription.SetUpForEquiped(items[0].name, items[0].description);
    }

    public void ButtonClick2()
    {
        button = 1;
        if (coroutine || items[1] == null) return;
        invDescription.SetUpForEquiped(items[1].name, items[1].description);
    }

    public void ShowEquiped()
    {
        int count = 1;
        foreach (Item item in items)
        {
            var weapon = transform.Find("Weapon" + count).GetComponent<UnityEngine.UI.Button>();
            var icon = weapon.transform.Find("EquipedIcon" + count).GetComponent<UnityEngine.UI.Image>();
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
                Debug.Log("Isnull");
                Inventory.Instance.Add(items[0]);
            }
            items[0] = tempItem;
            weaponSwitch.DeleteWeapon(0);
            weaponSwitch.AddWeapon(items[0].name, true);
            //Debug.Log(items[0]);
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
                Debug.Log("Isnull");
                Inventory.Instance.Add(items[1]);
            }
            items[1] = tempItem;
            weaponSwitch.DeleteWeapon(1);
            weaponSwitch.AddWeapon(items[1].name, false);
            //Debug.Log(items[1]);
            Inventory.Instance.ListItems();
            invDescription.Close();
            openInv.letDisable = true;
            ShowEquiped();
        }
        // ...
    }
}
