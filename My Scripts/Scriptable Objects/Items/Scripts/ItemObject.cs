using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum ItemType
{
    RangeWeapon,
    MeleeWeapon,
    Potion,
    Helmet,
    Chest,
    Boots

}

public enum Buffs
{
    HP,
    Damage
}

public abstract class ItemObject : ScriptableObject
{
    public int id;
    public Sprite uiDisplay;
    public ItemType type;
    [TextArea(15,20)]
    public string description;
    public ItemBuff[] buffs;

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}


[System.Serializable]
public class Item
{
    public string Name;
    public int id;
    public ItemBuff[] buffs;
    public Item(ItemObject item)
    {
        Name = item.name;
        id = item.id;
        buffs = new ItemBuff[item.buffs.Length];

        for(int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(item.buffs[i].min, item.buffs[i].max)
            {
                buff = item.buffs[i].buff
            };
        }
    }
}

[System.Serializable]
public class ItemBuff
{
    public Buffs buff;
    public int value;
    public int min;
    public int max;

    public ItemBuff(int _min , int _max)
    {
        min = _min;
        max = _max;
        GenerateValue();
    }
    public void GenerateValue()
    {
        value = UnityEngine.Random.Range(min,max);
    }
}