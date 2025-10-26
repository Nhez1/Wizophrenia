using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Singleton;
    public static UIItem carriedItem;

    [SerializeField] ItemSlot[] _slots;

    [SerializeField] UIItem _itemPrefab;

    [SerializeField] DialogueData _itemGetMsg;

    private void Awake()
    {
        Singleton = this;
    }

    public void SetCarriedItem(UIItem item)
    {
        // If clicking an item while carrying another item, return the latter to its slot.
        if (carriedItem != null) item.ActiveSlot.SetItem(carriedItem);

        carriedItem = item;
    }

    void AddItem(ItemSO item)
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            //Check if the slot is empty
            if (_slots[i].UIItem == null)
            {
                Instantiate(_itemPrefab, _slots[i].transform).Initialize(item, _slots[i]);
                break;
            }
        }
    }

    //void GetItemHeadsUp(ItemSO item)
    //{
    //    _itemGetMsg.lines[0] = $"Picked up {item.Name}";
    //    DialogueManager.Instance.StartDialogue(_itemGetMsg);
    //}

    private void OnEnable()
    {
        InternalInventory<ItemSO>.OnItemAdded += AddItem;
        //InternalInventory<ItemSO>.OnItemAdded += GetItemHeadsUp;
    }

    private void OnDisable()
    {
        InternalInventory<ItemSO>.OnItemAdded -= AddItem;
        //InternalInventory<ItemSO>.OnItemAdded -= GetItemHeadsUp;
    }
}