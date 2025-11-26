using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private ItemSlot[] _slots;
    [SerializeField] private ItemSlot _handSlot;

    [SerializeField] private UIItem _itemPrefab;

    [SerializeField] private DialogueData _itemGetMsg;

    public void AddItem(ItemSO item)
    {
        if (_handSlot.UIItem == null) UIItemFactory.Instance.CreateItem(item, _handSlot);
        else
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
    }

    //void GetItemHeadsUp(ItemSO item)
    //{
    //    _itemGetMsg.lines[0] = $"Picked up {item.Name}";
    //    DialogueManager.Instance.StartDialogue(_itemGetMsg);
    //}
}