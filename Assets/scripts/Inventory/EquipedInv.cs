using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class EquipedInv : MonoBehaviour
{
    public GameObject weaponHolder;
    public Item[] items = new Item[2];

    public Item tempItem = null;

    public UnityEngine.UI.Button weapon1;
    public UnityEngine.UI.Button weapon2;
    private Sprite[] sprites = new Sprite[2];

    public Sprite defaultSprite1, defaultSprite2;

    public OpenInventory openInv;

    public InvDescription invDescription;

    public UnityEngine.UI.Image currentWeaponIcon;

    public int button;

    private void Awake()
    {
        sprites[0] = defaultSprite1;
        sprites[1] = defaultSprite2;
    }

    private void Start()
    {
        ShowEquiped();
    }
    public void Add(Item item,  int index)
    {
        items[index] = item;

    }

    public void Remove(Item item, int index)
    {
        items[index] = null;
    }

    public void ListItems()
    {
        int currentindex = 0;
        GameObject[] weapons = weaponHolder.GetComponentsInChildren<GameObject>();
        foreach (Item item in items)
        {
            //item = weapons[currentindex];
        }
    }

    public void ShowEquiped()
    {
        int count = 1;
        foreach (Item item in items)
        {
            var weapon = transform.Find("Weapon" + count).GetComponent<UnityEngine.UI.Button>();
            var icon = weapon.transform.Find("EquipedIcon" + count).GetComponent<UnityEngine.UI.Image>();
            if (item != null)
            {
                icon.sprite = item.icon;
                icon.color = new Color32 (255, 255, 255, 255);
            }
            else
            {
                icon.sprite = sprites[count-1];
                icon.color = new Color32(255, 255, 255, 50);
            }
            count++;
        }
    }

    public void ReturnButtonClick()
    {
        StartCoroutine(GetItemSelect());
    }

    public void CheckForClick1()
    {
        button = 1;
    }

    public void CheckForClick2()
    {
        button = 2;
    }

    public void ChangeCurrentWeaponIcon(int index)
    {
        //currentWeaponIcon.sprite = items[index].icon;
    }

    IEnumerator GetItemSelect()
    {
        // ...
        var waitForButton = new WaitForUIButtons(weapon1, weapon2);
        yield return waitForButton.Reset();
        if (waitForButton.PressedButton == weapon1)
        {
            Debug.Log("1");
            items[0] = tempItem;
            Debug.Log(items[0]);
            Inventory.Instance.ListItems();
            invDescription.Close();
            openInv.letDisable = true;
            ShowEquiped();
        }
        else
        {
            Debug.Log("2");
            items[1] = tempItem;
            Debug.Log(items[1]);
            Inventory.Instance.ListItems();
            invDescription.Close();
            openInv.letDisable = true;
            ShowEquiped();
        }
        // ...
    }
}
