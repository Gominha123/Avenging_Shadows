using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    public WeaponController SelectedWeapon {  get; set; }

    private void Awake()
    {
        Instance = this;
    }
    public void Decorate(WeaponController weaponController)
    {
        if(SelectedWeapon.weapon is WeaponDecorator decorator)
        {
            decorator.Decorate(weaponController.weapon);
            weaponController.weapon = decorator;
        }
    }
}
