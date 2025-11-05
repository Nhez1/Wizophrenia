using UnityEngine;

public class UIAlchemy : MonoBehaviour
{
    [Header(" Crafting Slots ")]
    [SerializeField] private ItemSlot _itemSlot1;
    [SerializeField] private ItemSlot _itemSlot2;
    [SerializeField] private ItemSlot _itemSlot3;

    [Header(" System ")]
    [SerializeField] private AlchemySO _alchemy;

    [Header(" Result ")]
    [SerializeField] private ItemSlot _resultSlot;
    [SerializeField] private Sprite _resultIcon;


    public void DoAlchemy()
    {
        if (_itemSlot1.UIItem != null && _itemSlot2.UIItem != null && _itemSlot3.UIItem != null)
        {
            var herbA = _itemSlot1.UIItem.Item as HerbSO;
            var herbB = _itemSlot2.UIItem.Item as HerbSO;
            var herbC = _itemSlot3.UIItem.Item as HerbSO;

            var result = _alchemy.Mix(herbA, herbB, herbC);
            UIItemFactory.Instance.CreateItem(result, _resultSlot, _resultIcon);

            Destroy(_itemSlot1.UIItem.gameObject);
            Destroy(_itemSlot2.UIItem.gameObject);
            Destroy(_itemSlot3.UIItem.gameObject);
        }
    }
}