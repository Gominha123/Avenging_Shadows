using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponDecorator : IWeapon
{
    protected IWeapon weapon;

    protected readonly float damage;

    protected WeaponDecorator(float damage)
    {
        this.damage = damage;
    }

    public void Decorate(IWeapon weapon)
    {
        this.weapon = weapon;
    }

    public virtual float UpdateDamage()
    {
        return (float)(weapon?.UpdateDamage() + damage ?? damage);
    }
    //protected IWeapon weapon;

}

public class ToothDecorator : WeaponDecorator
{
    public ToothDecorator(float damage) : base(damage) { }
}
