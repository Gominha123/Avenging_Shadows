using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemControl : MonoBehaviour
{
    //public GameObject RemoveButton;
    public GameObject Button;

    //public void ButtonClick()
    //{
    //    bool notKey = Inventory.Instance.FindItemIndex(RemoveButton);
    //    if (notKey)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    public void ButtonClick()
    {
        Inventory.Instance.FindItemIndex(Button);//, false);

    }

    public void ButtonClickDelete()
    {
        Inventory.Instance.FindItemIndex(null);//, true);
    }
}
