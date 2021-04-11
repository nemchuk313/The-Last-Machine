 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database" , menuName = "Inventory System/Items/Database")]
public class ItemDataBaseObject : ScriptableObject, ISerializationCallbackReceiver
{

    //list of items that exist in our game
    public ItemObject[] items;

    public Dictionary<int , ItemObject> getItem = new Dictionary<int , ItemObject>();

    public void OnAfterDeserialize()
    { 
        for (int i = 0 ; i < items.Length; i ++ )
        {
            items[i].id = i;
            getItem.Add(i , items[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        getItem = new Dictionary<int, ItemObject>();
    }
}
