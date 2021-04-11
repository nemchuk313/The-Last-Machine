using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savePath;

    public  ItemDataBaseObject database;

    public Inventory Container;



    public void AddItem(Item _item, int _amount)
    {
        
        if(_item.buffs.Length > 0)
        {
            SetFirstEmptySlot(_item, _amount);
            return;
        }

        //Go through all the list of Inventory Slots and check if any contains current item
        for(int i = 0; i < Container.Items.Length; i ++)
        {
            //if we found such,just increase the amount of it in a current slot;
            if(Container.Items[i].id == _item.id)
            {
                Container.Items[i].AddAmount(_amount);
                return;
            }
        }

        SetFirstEmptySlot(_item, _amount);
    }

    public InventorySlot SetFirstEmptySlot( Item _item , int _amount )
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if(Container.Items[i].id <= -1)
            {
                Container.Items[i].UpdateSlot(_item.id, _item, _amount);
                return Container.Items[i];
            }
        }
        //set up functionality to what happens when the inventory is full
        return null;
    }

    public void Save()
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath) , FileMode.Create , FileAccess.Write);
        formatter.Serialize(stream , Container);
        stream.Close();
    }

    public void Load()
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Inventory newContainer = (Inventory)formatter.Deserialize(stream);
            for (int i = 0; i < Container.Items.Length; i++)
            {
                Container.Items[i].UpdateSlot(newContainer.Items[i].id, newContainer.Items[i].item, newContainer.Items[i].amount);
            }
            stream.Close();
        }
    }

    public void Clear()
    {
        Container = new Inventory();
    }

    public void MoveItem(InventorySlot item1, InventorySlot item2)
    {
        InventorySlot temp = new InventorySlot(item2.id, item2.item, item2.amount);
        item2.UpdateSlot(item1.id, item1.item, item1.amount);
        item1.UpdateSlot(temp.id, temp.item, temp.amount);
    }

    public void RemoveItem(Item _item)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].item == _item)
            {
                Container.Items[i].UpdateSlot(-1, null, 0);
            }
        }
    }

}

[System.Serializable]

public class Inventory
{
    public InventorySlot[] Items = new InventorySlot[35];
}


[System.Serializable]
public class InventorySlot
{
    public int id = -1;
    public Item item;
    public int amount;

    public InventorySlot()
    {
        id = -1;
        item = null;
        amount = 0;
    }

    //Konstruktor to set values when an inventory slot is created
    public InventorySlot( int _id , Item _item , int _amount)
    {
        id = _id;
        item = _item;
        amount = _amount;
    }

    public void UpdateSlot(int _id, Item _item, int _amount)
    {
        id = _id;
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int _value)
    {
        amount += _value;
    }
}
