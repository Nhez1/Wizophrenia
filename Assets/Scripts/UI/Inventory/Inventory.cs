using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField] private ItemSlot[] _slots;

    [SerializeField] private UIItem _itemPrefab;

    [SerializeField] private DialogueData _itemGetMsg;

    public void AddItem(ItemSO item)
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            //Check if the slot is empty
            if (_slots[i].UIItem == null)
            {
                UIItemFactory.Instance.CreateItem(item, _slots[i]);
                break;
            }
        }
    }

    //void GetItemHeadsUp(ItemSO item)
    //{
    //    _itemGetMsg.lines[0] = $"Picked up {item.Name}";
    //    DialogueManager.Instance.StartDialogue(_itemGetMsg);
    //}
}