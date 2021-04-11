using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Range Weapon Object", menuName = "Inventory System/Items/Range Weapon")]
public class RangeWeaponObject : ItemObject
{

    public int damage;
    public string range;
    public string fireRate;

    public void Awake()
    {
        type = ItemType.RangeWeapon;
    }
}
