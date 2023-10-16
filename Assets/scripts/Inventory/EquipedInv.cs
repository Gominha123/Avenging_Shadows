using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class EquipedInv : MonoBehaviour
{
    public GameObject weaponHolder;
    public Item[] items = new Item[2];

    public UnityEngine.UI.Button weapon1;
    public UnityEngine.UI.Button weapon2;

    public OpenInventory openInv;

    public int button;

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

    IEnumerator GetItemSelect()
    {
        // ...
        var waitForButton = new WaitForUIButtons(weapon1, weapon2);
        yield return waitForButton.Reset();
        if (waitForButton.PressedButton == weapon1)
        {
            Debug.Log("1");
            Inventory.Instance.ListItems();
            openInv.letDisable = true;
        }
        else
        {
            Debug.Log("2");
            Inventory.Instance.ListItems();
            openInv.letDisable = true;
        }
        // ...
    }
}
