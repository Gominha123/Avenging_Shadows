using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class WeaponSwitch : MonoBehaviour
{
    //public IWeapon Weapon {  get; set; }

    public int selectedWeapon = 0;

    public EquipedInv equipedInv;

    PlayerMovement2 playerMovement;
    WeaponController weaponController;
    public InteractionPromptUI interactionPromptUI;

    public AnimatorOverrideController animSword;
    public AnimatorOverrideController animBFSword;
    public AnimatorOverrideController animSpear;

    public int onDeleteIndex = 0;
    private bool onDelete0 = false;
    private bool onDelete1 = false;


    public int onDeleteNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement2>();

        StartingWeapons();
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
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
            if (count == i)
            {
                Destroy(weapon.gameObject);
                return;
            }
            count++;

        }
    }

    public void DeleteEquiped(int i)
    {
        int count = 0;
        GameObject obj = new GameObject("Hold" + i);
        onDeleteIndex = i;
        foreach (Transform weapon in transform)
        {
            if (count == i)
            {
                Destroy(weapon.gameObject);
                obj.transform.parent = transform;
                if (i == 0)
                {
                    obj.transform.SetAsFirstSibling();
                }
                else
                {
                    obj.transform.SetAsLastSibling();
                }
                return;
            }
            count++;
        }
        //if (selectedWeapon == onDeleteIndex)
        //{
        //    if (selectedWeapon == 0)
        //    {
        //        selectedWeapon = 1;
        //    }
        //    else
        //    {
        //        selectedWeapon = 0;
        //    }
        //    SelectWeapon();
        //}
    }

    public void DeleteEquipedOnDurability(Transform currentWeapon)
    {
        int i = 0;
        //GameObject obj = new GameObject("Hold" + i);
        foreach (Transform weapon in transform)
        {
            if (currentWeapon == weapon)
            {
                interactionPromptUI.SetUp("Weapon Broke");
                StartCoroutine(DoAfterFiveSeconds());
                GameObject obj = new GameObject("Hold" + i);
                Destroy(weapon.gameObject);
                obj.transform.parent = transform;
                if (i == 0)
                {
                    obj.transform.SetAsFirstSibling();
                }
                else
                {
                    obj.transform.SetAsLastSibling();
                }
                return;
            }
            i++;
        }
    }

    public void AddWeapon(string weaponName, bool firstSibling, float damage, int durability)
    {
        GameObject weaponPrefab = (GameObject)Resources.Load("Weapons/" + weaponName);
        GameObject weapon = Instantiate(weaponPrefab);
        weapon.layer = 0;
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

        if (firstSibling)
        {
            weapon.transform.SetAsFirstSibling();
        }
        else
        {
            weapon.transform.SetAsLastSibling();
        }
    }

    public void UpgradeWeapon(int i, GameObject tooth)
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
                    WeaponManager.Instance.SelectedWeapon = toothcontroller;
                    WeaponManager.Instance.Decorate(weaponController);
                    WeaponManager.Instance.SelectedWeapon.weapon.UpdateDamage();
                    weaponController.damage = weaponController.UpdateDamage();

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
        }
        else if (chooseWeaponVar == "Sword")
        {
            playerMovement.anim.runtimeAnimatorController = animSword;
        }
        else if (chooseWeaponVar == "Spear")
        {
            playerMovement.anim.runtimeAnimatorController = animSpear;
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
