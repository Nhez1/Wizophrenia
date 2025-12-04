using System.Collections.Generic;
using UnityEngine;

public class Inventory : MementoEntity
{
    [SerializeField] private ItemSlot[] _slots;

    [SerializeField] private UIItem _itemPrefab;

    [SerializeField] private DialogueData _itemGetMsg;

    private Dictionary<int, ItemSO> _invSave;

    protected override void Awake()
    {
        base.Awake();
        _invSave = new();
    }

    public void AddItem(ItemSO item)
    {
        // Primero chequear si el handSlot está libre
        foreach (var slot in _slots)
        {
            if (slot.IsHandSlot && slot.UIItem == null)
            {
                UIItemFactory.Instance.CreateItem(item, slot);
                return;
            }
        }

        // En caso de no estarlo, que caiga en cualquier slot libre.
        foreach (var slot in _slots)
        {
            if (slot.UIItem == null)
            {
                UIItemFactory.Instance.CreateItem(item, slot);
                return;
            }
        }

    }

    //void GetItemHeadsUp(ItemSO item)
    //{
    //    _itemGetMsg.lines[0] = $"Picked up {item.Name}";
    //    DialogueManager.Instance.StartDialogue(_itemGetMsg);
    //}

    protected override void SaveStates()
    {
        var loopTime = 0;

        foreach (var slot in _slots)
        {
            if (slot.UIItem != null) _invSave[loopTime] = slot.UIItem.Item;
            else _invSave[loopTime] = null;

            loopTime++;
        }

        _memento.SaveMemory(_invSave);
    }

    protected override void LoadStates(object[] oldState)
    {
        _invSave = (Dictionary<int, ItemSO>)oldState[0];

        for (int i = 0; i < _slots.Length; i++)
        {
            if (_invSave.TryGetValue(i, out var item) && item != null)
            {
                UIItemFactory.Instance.CreateItem(item, _slots[i]);
            }
        }
    }

}