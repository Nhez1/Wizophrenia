using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField] private ItemSlot[] _slots;

    [SerializeField] private UIItem _itemPrefab;

    [SerializeField] private DialogueData _itemGetMsg;

    [SerializeField] private CanvasGroup _self;

    private void Start()
    {
        _self = GetComponent<CanvasGroup>();
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