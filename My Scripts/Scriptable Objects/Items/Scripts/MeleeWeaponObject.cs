using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Weapon Object", menuName = "Inventory System/Items/Melee Weapon")]
public class MeleeWeaponObject : ItemObject
{

    public int damage;
    public string range;

    public void Awake()
    {
        type = ItemType.MeleeWeapon;
    }
}