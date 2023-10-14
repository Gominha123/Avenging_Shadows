using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemControl : MonoBehaviour
{
    public GameObject Button;

    public void ButtonClick()
    {
        Inventory.Instance.FindItemIndex(Button);

    }

    public void ButtonClickDelete()
    {
        Inventory.Instance.FindItemIndex(null);
    }

    public void ButtonCLickUse()
    {
        Inventory.Instance.FindItemIndex(null);
    }
}
