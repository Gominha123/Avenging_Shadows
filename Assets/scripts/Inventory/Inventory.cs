using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public List<Item> items = new List<Item>();

    public Transform itemContent;
    public GameObject inventoryItem;

    public EquipedInv equipedInventory;

    public InvDescription invDesciption;

    public Item selectedItem = null;

    public UnityEngine.UI.Button weapon1;
    public UnityEngine.UI.Button weapon2;

    public OpenInventory openInv;

    int currentIndex = 0;
    int equipedInventoryIndex = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if(equipedInventoryIndex != 0) {
            Debug.Log(equipedInventoryIndex);
        }
    }

    public void Add(Item item)
    {
        items.Add(item);
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }

    public void ListItems()
    {
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (Item item in items)
        {
            var obj = Instantiate(inventoryItem, itemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<UnityEngine.UI.Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }
    }

    public bool FindById(int id)
    {
        foreach (Item item in items)
        {
            if (item.id == id) return true;

        }
        return false;
    }

    public void FindItemIndex(GameObject button)
    {
        if (button != null)
        {
            currentIndex = button.transform.GetSiblingIndex();
            bool interactable = false;
            if (items[currentIndex].itemType == Item.ItemType.KeyItem) { }
            else interactable = true;
            invDesciption.SetUp(items[currentIndex].itemName, items[currentIndex].description, interactable);
            selectedItem = items[currentIndex];

        }
        else
        {
            switch (items[currentIndex].itemType)
            {
                case Item.ItemType.KeyItem:
                    return;
                case Item.ItemType.Weapon:
                    //equipedInventory.button = 0;
                    equipedInventoryIndex = 0;
                    DisableItemButtons();
                    equipedInventory.ReturnButtonClick();
                    equipedInventory.tempItem = items[currentIndex];
                    Remove(items[currentIndex]);
                    break;
                case Item.ItemType.NotKeyItem:
                    Destroy(button);
                    Remove(items[currentIndex]);
                    ListItems();
                    invDesciption.Close();
                    break;
            }
            
        }
    }

    public static GameObject FindParentWithTag(GameObject childObject, string tag)
    {
        Transform t = childObject.transform;
        while (t.parent != null)
        {
            if (t.parent.tag == tag)
            {
                return t.parent.gameObject;
            }
            t = t.parent.transform;
        }
        return null; // Could not find a parent with given tag.
    }

    public void DisableItemButtons()
    {
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
            openInv.letDisable = false;
            invDesciption.SetUp(null, "Please Select an Equiped Item Slot", false);
        }
    }

}
