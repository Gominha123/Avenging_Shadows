using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public int selectedWeapon = 0;

    public EquipedInv equipedInv;

    public int onDeleteIndex = 0;
    private bool onDelete0 = false;
    private bool onDelete1 = false;

    public int onDeleteNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(transform.childCount == 0)
        {
            onDelete0 = true;
            onDelete1 = true;
        }
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
        foreach(Transform weapon in transform)
        {
            equipedInv.Add(weapon.GetComponent<ItemController>().item, count);
            count++;
        }
        if(count < 1)                               /////escrever
        {

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
            Debug.Log("Here");
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
        Debug.Log(i);
        foreach (Transform weapon in transform)
        {
            if(count == i)
            {
                Debug.Log(weapon.gameObject.name);
                Destroy(weapon.gameObject);
                obj.transform.parent = transform;
                if(i == 0)
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

    public void AddWeapon(string weaponName, bool firstSibling)
    {
        GameObject weaponPrefab = (GameObject)Resources.Load("Weapons/"+ weaponName);
        GameObject weapon = Instantiate(weaponPrefab);
        Vector3 tempPos = weapon.transform.position;
        Quaternion tempRot = weapon.transform.rotation;
        weapon.transform.parent = transform;
        weapon.transform.SetLocalPositionAndRotation(tempPos, tempRot);
        if(firstSibling)
        {
            weapon.transform.SetAsFirstSibling();
        }
        else
        {
            weapon.transform.SetAsLastSibling();
        }
    }
}
