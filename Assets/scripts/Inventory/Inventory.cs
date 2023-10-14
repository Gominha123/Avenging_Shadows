using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public List<Item> items = new List<Item>();

    public Transform itemContent;
    public GameObject inventoryItem;

    public InvDescription invDesciption;

    public Item selectedItem = null;

    int currentIndex = 0;

    private void Awake()
    {
        Instance = this;
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
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var deleteButton = obj.transform.Find("DeleteButton").GetComponent<Button>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
            deleteButton.gameObject.SetActive(true);
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

    //public bool FindItemIndex(GameObject button)
    //{
    //    GameObject currentItem = FindParentWithTag(button, "Item");
    //    int currentIndex = currentItem.transform.GetSiblingIndex();
    //    if (items[currentIndex].itemType == Item.ItemType.KeyItem)
    //    {
    //        return false;
    //    }
    //    else
    //    {
    //        Remove(items[currentIndex]);
    //        ListItems();
    //        return true;
    //    }
    //}

    public void FindItemIndex(GameObject button)//, bool use)
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
            if (items[currentIndex].itemType == Item.ItemType.KeyItem)
            {
                return;
            }
            Destroy(button);
            Remove(items[currentIndex]);
            ListItems();
        }
        //if (use)
        //{
        //    Debug.Log("up");
        //    if (items[currentIndex].itemType == Item.ItemType.KeyItem)
        //    {
        //        return;
        //    }
        //    Destroy(button);
        //    Remove(items[currentIndex]);
        //    ListItems();
        //}
        //else
        //{
        //    bool interactable = false;
        //    if (items[currentIndex].itemType == Item.ItemType.KeyItem) { }
        //    else interactable = true;
        //    invDesciption.SetUp(items[currentIndex].itemName, items[currentIndex].description, interactable);
        //    selectedItem = items[currentIndex];
        //}
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


}
