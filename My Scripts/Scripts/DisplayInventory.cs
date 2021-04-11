using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DisplayInventory : MonoBehaviour
{
    public MouseItem mouseItem = new MouseItem();

    public GameObject inventoryPrefab;
    public InventoryObject inventory;//Place for inventory we r going to display

    public int x_start;
    public int y_start;

    public int x_space_between_items;
    public int y_space_between_items;
    public int number_of_columns;

    Dictionary<GameObject , InventorySlot> itemsDisplayed = new Dictionary<GameObject , InventorySlot>();

    // Start is called before the first frame update
    void Start()
    {
        CreateSlots();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlots();
    }

    public void UpdateSlots()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
        {
            if (_slot.Value.id >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.getItem[_slot.Value.item.id].uiDisplay;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1,1,1,1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }

    public void CreateSlots()
    {
        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();

        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = Instantiate( inventoryPrefab , Vector3.zero , Quaternion.identity , transform );
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

            itemsDisplayed.Add(obj, inventory.Container.Items[i]);
        }
    }

    private void AddEvent( GameObject _obj , EventTriggerType _type , UnityAction<BaseEventData> _action)
    {
        EventTrigger trigger = _obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = _type;
        eventTrigger.callback.AddListener(_action);
        trigger.triggers.Add(eventTrigger);
    }

    public void OnEnter(GameObject _obj)
    {
        mouseItem.hoverObj = _obj;
        if (itemsDisplayed.ContainsKey(_obj))
            mouseItem.hoverItem = itemsDisplayed[_obj];
    }

    public void OnExit(GameObject _obj)
    {
        mouseItem.hoverObj = null;
        mouseItem.hoverItem = null;
    }

    public void OnDragStart(GameObject _obj)
    {
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(50, 50);
        mouseObject.transform.SetParent(transform.parent);

        if (itemsDisplayed[_obj].id >= 0)
        {
            var img = mouseObject.AddComponent<Image>();
            img.sprite = inventory.database.getItem[itemsDisplayed[_obj].id].uiDisplay;
            img.raycastTarget = false;
        }

        mouseItem.obj = mouseObject;
        mouseItem.item = itemsDisplayed[_obj];
    }

    public void OnDragEnd(GameObject _obj)
    {
        if(mouseItem.hoverObj)
        {
            inventory.MoveItem(itemsDisplayed[_obj], itemsDisplayed[mouseItem.hoverObj]);
        }
        else
        {
            inventory.RemoveItem(itemsDisplayed[_obj].item);
        }

        Destroy(mouseItem.obj);
        mouseItem.item = null;
    }

    public void OnDrag(GameObject _obj)
    {
        if(mouseItem.obj != null)
        {
            mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3( x_start + (x_space_between_items * (i % number_of_columns)) , y_start + (-y_space_between_items *(i / number_of_columns)) , 0f );
    }

}

public class MouseItem
{
    public GameObject obj;
    public InventorySlot item;
    public InventorySlot hoverItem;
    public GameObject hoverObj;
}
