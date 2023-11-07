using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon/Create New Weapon")]
public class Weapon : ScriptableObject
{
    public float upgradeDamage;
    public int durability;
    public WeaponType weaponType;

}

public enum WeaponType
{
    weapon,
    tooth
}

public static class WeaponFactory
{
    public static IWeapon Create(Weapon weapon)
    {
        return weapon.weaponType switch { WeaponType.tooth => new ToothDecorator(weapon.upgradeDamage),
        WeaponType.weapon => new CurrentWeapon(weapon.upgradeDamage) };
    }
}
