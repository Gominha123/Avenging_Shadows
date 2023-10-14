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
            if (items[currentIndex].itemType == Item.ItemType.KeyItem)
            {
                return;
            }
            Destroy(button);
            Remove(items[currentIndex]);
            ListItems();
            invDesciption.Close();
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


}
