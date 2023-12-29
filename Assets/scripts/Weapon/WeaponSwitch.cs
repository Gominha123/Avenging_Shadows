using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    //public IWeapon Weapon {  get; set; }

    public bool isScrollable = true;

    public int selectedWeapon = 0;

    public EquipedInv equipedInv;

    public InvDescription invDescription;

    PlayerMovement2 playerMovement;
    WeaponController weaponController;
    public InteractionPromptUI interactionPromptUI;

    public AnimatorOverrideController animSword;
    public AnimatorOverrideController animBFSword;
    public AnimatorOverrideController animSpear;

    public int onDeleteIndex = 0;
    private bool onDelete0 = false;
    private bool onDelete1 = false;

    private int selectedAnim;

    public int onDeleteNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement2>();
        selectedAnim = -1;
        StartingWeapons();
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if(isScrollable)
        {
            int previousSelectedWeapon = selectedWeapon;
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (selectedWeapon >= transform.childCount - 1)
                    selectedWeapon = 0;
                else
                    selectedWeapon++;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (selectedWeapon <= 0)
                    selectedWeapon = transform.childCount - 1;
                else
                    selectedWeapon--;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                selectedWeapon = 0;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
            {
                selectedWeapon = 1;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
            {
                selectedWeapon = 2;
            }

            if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
            {
                selectedWeapon = 3;
            }



            if (previousSelectedWeapon != selectedWeapon)
            {
                SelectWeapon();
            }
        }
        

    }

    public void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                equipedInv.ChangeCurrentWeaponIcon(i);

                ChooseAnimator(weapon.tag);

                playerMovement.wp = weapon.GetComponent<WeaponController>();
            }
            else
                weapon.gameObject.SetActive(false);
            i++;

        }
    }

    public void StartingWeapons()
    {
        Item items;
        int count = 0;
        foreach (Transform weapon in transform)
        {
            //
            equipedInv.Add(weapon?.GetComponent<ItemController>()?.item, count);
            weapon.gameObject.layer = 0;
            weapon.GetComponent<WeaponController>().SetWeapon();
            count++;
        }
    }

    public void DeleteWeapon(int i)
    {
        int count = 0;
        //onDeleteIndex = i;
        //onDelete = true;
        //if(selectedWeapon == onDeleteIndex)
        //{
        //    if(selectedWeapon == 0)
        //    {
        //        selectedWeapon = 1;
        //    }
        //    else
        //    {
        //        selectedWeapon = 0;
        //    }
        //    SelectWeapon();
        //}
        foreach (Transform weapon in transform)
        {
            //Debug.Log("Here");
            if (count == i )//&& weapon.name != "Hold")
            {
                Destroy(weapon.gameObject);
                return;
            }
            count++;

        }
    }

    //public void DeleteEquiped(int i)
    //{
    //    int count = 0;
    //    GameObject obj = new GameObject("Hold");// + i);
    //    if (isDeletable)
    //    {
    //        onDeleteIndex = i;
    //        foreach (Transform weapon in transform)
    //        {
    //            if (count == i)
    //            {
    //                Destroy(weapon.gameObject);
    //                obj.transform.parent = transform;
    //                if (i == 0)
    //                {
    //                    obj.transform.SetAsFirstSibling();
    //                }
    //                else
    //                {
    //                    obj.transform.SetAsLastSibling();
    //                }
    //                isDeletable = false;
    //                return;
    //            }
    //            count++;
    //        }
    //    }
    //    else
    //    {
    //        Destroy(obj.gameObject);
    //        interactionPromptUI.SetUp("You only have this weapon make sure you dont lose it");
    //        StartCoroutine(DoAfterFiveSeconds());
    //    }
    //}

    //public void DeleteEquipedOnDurability(Transform currentWeapon)
    //{
    //    int i = 0;
    //    if (isDeletable)
    //    {
    //        foreach (Transform weapon in transform)
    //        {
    //            if (currentWeapon == weapon)
    //            {
    //                interactionPromptUI.SetUp("Weapon Broke");
    //                StartCoroutine(DoAfterFiveSeconds());
    //                GameObject obj = new GameObject("Hold");
    //                Destroy(weapon.gameObject);
    //                obj.transform.parent = transform;
    //                if (i == 0)
    //                {
    //                    obj.transform.SetAsFirstSibling();
    //                }
    //                else
    //                {
    //                    obj.transform.SetAsLastSibling();
    //                }
    //                selectedWeapon++;
    //                isDeletable = false;
    //                return;
    //            }
    //            i++;
    //        }
    //    }
    //    else
    //    {
    //        interactionPromptUI.SetUp("Weapon Broke Find Another to Deal Damage");
    //        StartCoroutine(DoAfterFiveSeconds());
    //        currentWeapon.GetComponent<WeaponController>().damage = 0;
    //    }

    //}

    public void DeleteEquiped(int i)
    {
        int count = 0;
        if(transform.childCount == 1)
        {
            invDescription.SetUpForEquiped("", "You only have this weapon make sure you dont lose it", false);
            return;
        }
        GameObject obj = new GameObject("Hold");// + i);
        

        onDeleteIndex = i;
        foreach (Transform weapon in transform)
        {
            if (count == i)
            {
                if (transform.GetChild(1).name == "Hold")
                {
                    Destroy(obj.gameObject);
                    invDescription.SetUpForEquiped("", "You only have this weapon make sure you dont lose it", false);
                    return;
                }
                else
                {
                    
                    if (i == 0)
                    {
                        if (selectedWeapon == i)
                        {
                            selectedWeapon++;
                            SelectWeapon();
                        }
                        Destroy(weapon.gameObject);
                        obj.transform.parent = transform;
                        obj.transform.SetAsFirstSibling();
                        
                    }
                    else
                    {
                        if (selectedWeapon == i)
                        {
                            selectedWeapon--;
                            SelectWeapon();
                        }
                        Destroy(weapon.gameObject);
                        obj.transform.parent = transform;
                        obj.transform.SetAsLastSibling();
                    }
                    isScrollable = false;
                    invDescription.Close();
                    return;
                }
                
            }
            if (weapon.name == "Hold")
            {
                Destroy(obj.gameObject);
                invDescription.SetUpForEquiped("", "You only have this weapon make sure you dont lose it", false);
                return;
            }
            count++;
        }
    }

    public void DeleteEquipedOnDurability(Transform currentWeapon)
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (currentWeapon == weapon)
            {
                if (transform.GetChild(1).name == "Hold")
                {
                    interactionPromptUI.SetUp("Weapon Broke Find Another to Deal Damage");
                    StartCoroutine(DoAfterFiveSeconds());
                    currentWeapon.GetComponent<WeaponController>().damage = 0;
                    return;
                }
                else
                {
                    interactionPromptUI.SetUp("Weapon Broke");
                    StartCoroutine(DoAfterFiveSeconds());

                    equipedInv.button = i;
                    equipedInv.RemoveEquiped();
                    
                    //isScrollable = false;
                    //GameObject obj = new GameObject("Hold");
                    //obj.transform.parent = transform;
                    //if (i == 0)
                    //{
                    //    selectedWeapon++;
                    //    SelectWeapon();
                    //    obj.transform.SetAsFirstSibling();
                    //}
                    //else
                    //{
                    //    selectedWeapon--;
                    //    SelectWeapon();
                    //    obj.transform.SetAsLastSibling();
                    //}
                    //Destroy(weapon.gameObject);
                    //equipedInv.ChangeCurrentWeaponIcon(i);
                    //return;
                }
                
            }
            if (weapon.name == "Hold")
            {
                interactionPromptUI.SetUp("Weapon Broke Find Another to Deal Damage");
                StartCoroutine(DoAfterFiveSeconds());
                currentWeapon.GetComponent<WeaponController>().damage = 0;
                return;
            }
            i++;
        }
    }

    public void AddWeapon(string weaponName, bool firstSibling, float damage, int durability, int upgradeCount)
    {
        GameObject weaponPrefab = (GameObject)Resources.Load("Weapons/" + weaponName);
        GameObject weapon = Instantiate(weaponPrefab);
        weapon.layer = 6;
        Vector3 tempPos = weapon.transform.position;
        Quaternion tempRot = weapon.transform.rotation;
        weapon.transform.parent = transform;
        weapon.transform.SetLocalPositionAndRotation(tempPos, tempRot);
        WeaponController wpc = weapon.GetComponent<WeaponController>();
        Weapon wpcw = wpc.GetWeaponItem();
        wpcw.upgradeDamage = damage;
        wpcw.durability = durability;
        wpc.weaponItem = wpcw;
        wpc.SetWeapon();
        wpc.upCounter = upgradeCount;

        if (firstSibling)
        {
            weapon.transform.SetAsFirstSibling();
        }
        else
        {
            weapon.transform.SetAsLastSibling();
        }

        equipedInv.ChangeCurrentWeaponIcon(selectedWeapon);
        SelectWeapon();
    }

    public void UpgradeWeapon(int i, GameObject tooth, Item toothItem)
    {
        int count = 0;
        onDeleteIndex = i;
        WeaponController toothcontroller = tooth.GetComponent<WeaponController>();
        foreach (Transform weapon in transform)
        {
            if (count == i)
            {
                if (toothcontroller.weapon is WeaponDecorator decorator)
                {
                    WeaponController weaponController = weapon.GetComponent<WeaponController>();
                    if(weaponController.upCounter >= 3) 
                    {
                        invDescription.SetUp(null, "Weapon is Already Fully Upgraded", false, false);
                        return;
                    }
                    else
                    {
                        WeaponManager.Instance.SelectedWeapon = toothcontroller;
                        WeaponManager.Instance.Decorate(weaponController);
                        WeaponManager.Instance.SelectedWeapon.weapon.UpdateDamage();
                        weaponController.damage = weaponController.UpdateDamage();
                        Inventory.Instance.Remove(toothItem);
                    }
                    

                }
            }
            count++;
        }
    }

    private void ChooseAnimator(string chooseWeaponVar)
    {
        if (chooseWeaponVar == "BFSword")
        {
            playerMovement.anim.runtimeAnimatorController = animBFSword;
            selectedAnim = 0;
        }
        else if (chooseWeaponVar == "Sword")
        {
            playerMovement.anim.runtimeAnimatorController = animSword;
            selectedAnim = 1;
        }
        else if (chooseWeaponVar == "Spear")
        {
            playerMovement.anim.runtimeAnimatorController = animSpear;
            selectedAnim = 2;
        }
    }

    public void GetWeaponItem(int i) 
    {
        int count = 0;
        foreach (Transform weapon in transform)
        {
            //Debug.Log("Here");
            if (count == i)
            {
                WeaponController wpc = weapon.GetComponent<WeaponController>();
                equipedInv.tempOldWeaponDamage = wpc.damage;
                equipedInv.tempOldWeaponDurability = wpc.durability;
                equipedInv.tempOldWeaponUpgrade = wpc.upCounter;
            }
            count++;

        }
        //return null;
    }

    IEnumerator DoAfterFiveSeconds()
    {
        yield return new WaitForSeconds(5);

        interactionPromptUI.Close();

    }


}
