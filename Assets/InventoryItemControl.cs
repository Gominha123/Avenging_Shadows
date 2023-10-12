using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemControl : MonoBehaviour
{
    public GameObject RemoveButton;

    public void ButtonClick()
    {
        bool notKey = Inventory.Instance.FindItemIndex(RemoveButton);
        if (notKey)
        {
            Destroy(gameObject);
        }
    }
}
