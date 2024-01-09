using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public List<Item> items = new List<Item>();

    public List<Item> keyItems = new List<Item>();
    public List<Item> weaponItems = new List<Item>();
    public List<Item> upgradeItems = new List<Item>();
    public List<Item> potionItems = new List<Item>();

    public List<GameObject> teeth = new List<GameObject>();

    public float[] invWeaponDamage = new float[2];
    public int[] invWeaponDurability = new int[2];
    public int[] invWeaponUpgradedCount = new int[2];

    //public Item[] items = new Item[10];

    public Transform itemContent;
    public GameObject inventoryItem;

    public EquipedInv equipedInventory;

    public InvDescription invDesciption;

    //public Item selectedItem = null;

    public UnityEngine.UI.Button weapon1;
    public UnityEngine.UI.Button weapon2;

    public OpenInventory openInv;

    public PlayerHealth playerHealth;

    public Transform weaponContent;
    public Transform potionContent;
    public Transform keyItemContent;
    public Transform upgradeContent;

    public int weaponCount = 0;
    public int potionCount = 0;
    public int keyItemCount = 0;
    public int artifactCount = 0;
    public int upgradeCount = 0;

    int currentIndex = 0;
    int currentTypeIndex = 0;
    //int equipedInventoryIndex = 0;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject.transform.parent.gameObject);
            GameObject spawn = GameObject.Find("SpawnPoint");
            this.gameObject.transform.parent.transform.position = spawn.transform.position;
            return;
        }

        Instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject.transform.parent.gameObject);
    }

    //private void Start()
    //{
    //    Debug.Log(Instance);
    //    if(Instance != null)
    //    {
    //        Destroy(this.gameObject.transform.parent.gameObject);
    //        GameObject spawn = GameObject.Find("SpawnPoint");
    //        this.gameObject.transform.parent.transform.position = spawn.transform.position;
    //        return;
    //    }

    //    Instance = this;
    //    GameObject.DontDestroyOnLoad(this.gameObject.transform.parent.gameObject);
    //}

    public int Count() { return items.Count; }

    public void Add(Item item, [Optional] Weapon weapon)
    {
        switch (item.itemType)
        {
            case Item.ItemType.Weapon:weaponItems.Add(item); weaponCount++; break;//weapons.Add(weapon); Debug.Log(weapons[0].upgradeDamage); weaponCount++; break;
            case Item.ItemType.KeyItem:keyItems.Add(item);keyItemCount++; break;
            case Item.ItemType.Health:potionItems.Add(item);potionCount++; break;
            case Item.ItemType.Tooth:upgradeItems.Add(item);upgradeCount++; break;
            case Item.ItemType.Artefact:items.Add(item);artifactCount++; break;
            case Item.ItemType.NotKeyItem:items.Add(item); artifactCount++;break;
        }
        //items.Add(item);
        //for (int i = 0; i < 10; i++) {
        //    if (items[i] == null) {
        //        items[i] = item;
        //    }
        //}
    }

    public void Remove(Item item, [Optional] Weapon weapon)
    {
        switch (item.itemType)
        {
            case Item.ItemType.Weapon: weaponItems.Remove(item); weaponCount--; break; // Debug.Log(weapons[0].durability); weapons.Remove(weapon); weaponCount--; break;
            case Item.ItemType.KeyItem: keyItems.Remove(item); keyItemCount--; break;
            case Item.ItemType.Health: potionItems.Remove(item); potionCount--; break;
            case Item.ItemType.Tooth: upgradeItems.Remove(item); upgradeCount--; break;
            case Item.ItemType.Artefact: items.Remove(item); artifactCount--; break;
            case Item.ItemType.NotKeyItem: items.Remove(item); artifactCount--; break;
        }

        //for(int i = 0;i < 10;i++)
        //{
        //    if()
        //}

    }

    public void AddObject(GameObject tooth)
    {
        teeth.Add(tooth);
    }

    public void RemoveObject(GameObject tooth)
    {
        teeth.Remove(tooth);

    }

    public void ListItems()
    {
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (Transform item in weaponContent)
        {
            Destroy(item.gameObject);
        }

        foreach (Transform item in potionContent)
        {
            Destroy(item.gameObject);
        }

        foreach (Transform item in keyItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (Transform item in upgradeContent)
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
        foreach (Item item in keyItems)
        {
            var obj = Instantiate(inventoryItem, keyItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<UnityEngine.UI.Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }
        foreach (Item item in weaponItems)
        {
            var obj = Instantiate(inventoryItem, weaponContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<UnityEngine.UI.Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }
        foreach (Item item in upgradeItems)
        {
            var obj = Instantiate(inventoryItem, upgradeContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<UnityEngine.UI.Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }
        foreach (Item item in potionItems)
        {
            var obj = Instantiate(inventoryItem, potionContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<UnityEngine.UI.Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }
    }

    public bool FindById(int id, bool weapon)
    {
        if (!weapon)
        {
            foreach (Item item in keyItems)
            {
                if (item.id == id) return true;

            }
            return false;
        }
        else
        {
            foreach (Item item in weaponItems)
            {
                if (item.id == id) return true;

            }
            return false;
        }
        
    }

    public bool FindByIdWeapon(int id)
    {
        foreach (Item item in weaponItems)
        {
            if (item.id == id) return true;

        }
        return false;
    }

    //mudar
    public void FindItemIndex(GameObject button, [ Optional ] bool delete)
    {
        if (button != null)
        {
            currentTypeIndex = button.transform.parent.parent.GetSiblingIndex();
            currentIndex = button.transform.GetSiblingIndex();
            switch (currentTypeIndex)
            {
                case 0: invDesciption.SetUp(keyItems[currentIndex].itemName, keyItems[currentIndex].description, false, false); break; //keyitem
                case 1:
                    if (weaponItems[currentIndex].id == 12 || weaponItems[currentIndex].id == 13)
                    {
                        invDesciption.SetUp(weaponItems[currentIndex].itemName, weaponItems[currentIndex].description + "\nDamage: " + invWeaponDamage[currentIndex] + " Durability: " + "Infinite" + " Upgradability: " + invWeaponUpgradedCount[currentIndex] + "/3", true, false); break; //weapons
                    }
                    else
                    {
                        invDesciption.SetUp(weaponItems[currentIndex].itemName, weaponItems[currentIndex].description + "\nDamage: " + invWeaponDamage[currentIndex] + " Durability: " + invWeaponDurability[currentIndex] + " Upgradability: " + invWeaponUpgradedCount[currentIndex] + "/3", true, true); break; //weapons
                    }
                case 2: invDesciption.SetUp(potionItems[currentIndex].itemName, potionItems[currentIndex].description, true, true); break; //potions
                case 3: invDesciption.SetUp(upgradeItems[currentIndex].itemName, upgradeItems[currentIndex].description, true, true); break; //upgrade
                case 4: invDesciption.SetUp(items[currentIndex].itemName, items[currentIndex].description, false, true);break; //artefact
                default: break;//invDesciption.SetUp(items[currentIndex].itemName, items[currentIndex].description, true, true);break; //default
            }
            //currentIndex = button.transform.GetSiblingIndex();
            //Debug.Log(currentIndex);
            //switch (items[currentIndex].itemType)
            //{
            //    case Item.ItemType.KeyItem:
            //        invDesciption.SetUp(items[currentIndex].itemName, items[currentIndex].description, false, false);
            //        break;
            //    case Item.ItemType.Artefact:
            //        invDesciption.SetUp(items[currentIndex].itemName, items[currentIndex].description, false, true);
            //        break;
            //    default:
            //        invDesciption.SetUp(items[currentIndex].itemName, items[currentIndex].description, true, true);
            //        break;

            //}
            //bool interactable = false;
            //if (items[currentIndex].itemType == Item.ItemType.KeyItem) { }
            //else interactable = true;
            //invDesciption.SetUp(items[currentIndex].itemName, items[currentIndex].description, interactable);
            ////selectedItem = items[currentIndex];

        }
        else
        {
            switch (currentTypeIndex)
            {
                case 0://keyitem
                    if (delete)
                    {
                        Destroy(button);
                        Remove(keyItems[currentIndex]);
                        ListItems();
                        invDesciption.Close();
                    }
                    else
                    {
                        return;
                    }
                    break; 
                case 1://weapons
                    if (delete)
                    {
                        Destroy(button);
                        Remove(weaponItems[currentIndex]);
                        if(currentIndex == 0)
                        {
                            invWeaponDamage[0] = invWeaponDamage[1];
                            invWeaponDurability[0] = invWeaponDurability[1];
                            invWeaponDamage[1] = 0;
                            invWeaponDurability[1] = 0;
                        }
                        ListItems();
                        invDesciption.Close();
                    }
                    else
                    {
                        DisableItemButtons();
                        equipedInventory.tempItem = weaponItems[currentIndex];
                        equipedInventory.tempCurrentWeaponDamage = invWeaponDamage[currentIndex];
                        Debug.Log(invWeaponDamage[currentIndex]);
                        equipedInventory.tempCurrentWeaponDurability = invWeaponDurability[currentIndex];
                        equipedInventory.ReturnButtonClick();
                        Remove(weaponItems[currentIndex]);
                        if (currentIndex == 0)
                        {
                            invWeaponDamage[0] = invWeaponDamage[1];
                            invWeaponDurability[0] = invWeaponDurability[1];
                            invWeaponDamage[1] = 0;
                            invWeaponDurability[1] = 0;
                        }
                    }
                    break;
                case 2: //potions
                    if (delete)
                    {
                        Destroy(button);
                        Remove(potionItems[currentIndex]);
                        ListItems();
                        invDesciption.Close();
                    }
                    else
                    {
                        Destroy(button);
                        Remove(potionItems[currentIndex]);
                        ListItems();
                        invDesciption.Close();
                        playerHealth.GetHp();
                    }

                    break;
                case 3: //upgrade
                    if (delete)
                    {
                        Destroy(button);
                        Remove(upgradeItems[currentIndex]);
                        ListItems();
                        invDesciption.Close();
                    }
                    else
                    {
                        DisableItemButtons();
                        equipedInventory.toothObject = teeth[0];
                        equipedInventory.ReturnButtonClickForUpgrade();
                        //Remove(upgradeItems[currentIndex]);
                        equipedInventory.toothItem = upgradeItems[currentIndex];
                        RemoveObject(teeth[0]);
                    }
                    break;
                case 4: //artefact
                    if (delete)
                    {
                        Destroy(button);
                        Remove(items[currentIndex]);
                        ListItems();
                        invDesciption.Close();
                    }
                    else
                    {
                       
                    }
                    break;
                default: //default
                    if (delete)
                    {
                        Destroy(button);
                        Remove(items[currentIndex]);
                        ListItems();
                        invDesciption.Close();
                    }
                    else
                    {
                        //Destroy(button);
                        //Remove(items[currentIndex]);
                        //ListItems();
                        //invDesciption.Close();
                    }
                    break;
            }
            //if(delete)
            //{
            //    Destroy(button);
            //    Remove(items[currentIndex]);
            //    ListItems();
            //    invDesciption.Close();
            //}
            //else
            //{
            //    switch (items[currentIndex].itemType)
            //    {
            //        case Item.ItemType.KeyItem:
            //            return;
            //        case Item.ItemType.Weapon:
            //            //equipedInventory.button = 0;
            //            //equipedInventoryIndex = 0;
            //            DisableItemButtons();
            //            equipedInventory.tempItem = items[currentIndex];
            //            equipedInventory.ReturnButtonClick();
            //            Remove(items[currentIndex]);
            //            break;
            //        case Item.ItemType.NotKeyItem:
            //            Destroy(button);
            //            Remove(items[currentIndex]);
            //            ListItems();
            //            invDesciption.Close();
            //            break;
            //        case Item.ItemType.Health:
            //            Destroy(button);
            //            Remove(items[currentIndex]);
            //            ListItems();
            //            invDesciption.Close();
            //            playerHealth.GetHp();
            //            break;
            //        case Item.ItemType.Artefact:
            //            break;
            //        case Item.ItemType.Tooth:
            //            //Destroy(button);
            //            DisableItemButtons();
            //            equipedInventory.toothObject = teeth[0];
            //            equipedInventory.ReturnButtonClickForUpgrade();
            //            Remove(items[currentIndex]);
            //            RemoveObject(teeth[0]);

            //            break;
            //    }
            

                    
            //}
            
        }
    }

    public void FirstWeaponEquip()
    {
        DisableItemButtons();
        equipedInventory.tempItem = weaponItems[0];
        equipedInventory.tempCurrentWeaponDamage = invWeaponDamage[0];
        Debug.Log(invWeaponDamage[0]);
        equipedInventory.tempCurrentWeaponDurability = invWeaponDurability[0];
        equipedInventory.FirstWeaponEquip();
        Remove(weaponItems[0]);
        invWeaponDamage[0] = invWeaponDamage[1];
        invWeaponDurability[0] = invWeaponDurability[1];
        invWeaponDamage[1] = 0;
        invWeaponDurability[1] = 0;
    }

    public void SetUpDamageDurability(float damage, int durability, int alreadyUpgraded)
    {
        if (invWeaponDamage[0] == 0)
        {
            invWeaponDamage[0] = damage;
            invWeaponDurability[0] = durability;
            invWeaponUpgradedCount[0] = alreadyUpgraded;
            
        }
        else
        {
            invWeaponDamage[1] = damage;
            invWeaponDurability[1] = durability;
            invWeaponUpgradedCount[1] = alreadyUpgraded;
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
            //openInv.letDisable = false;
            //invDesciption.SetUp(null, "Please Select an Equiped Item Slot", false, false);
        }

        foreach (Transform item in weaponContent)
        {
            Destroy(item.gameObject);
        }

        foreach (Transform item in potionContent)
        {
            Destroy(item.gameObject);
        }

        foreach (Transform item in keyItemContent)
        {
            Destroy(item.gameObject);
        }

        openInv.letDisable = false;
        invDesciption.SetUp(null, "Please Select an Equiped Item Slot", false, false);
    }

}
