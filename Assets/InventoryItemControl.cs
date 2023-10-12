using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemControl : MonoBehaviour
{
    public Item item;
    public GameObject RemoveButton;

    public void AddItem(Item newItem)
    {
        Debug.Log("item");
        item = newItem;
    }

    public void ButtonClick()
    {
        Inventory.Instance.FindItemIndex(RemoveButton);
        Destroy(gameObject);
    }
}
