using UnityEngine;

public class UIAlchemy : MonoBehaviour
{
    [Header(" Crafting Slots ")]
    //[SerializeField] private ItemSlot _itemSlot1;
    [SerializeField] private ItemSlot _itemSlot2;
    //[SerializeField] private ItemSlot _itemSlot3;

    [Header(" System ")]
    [SerializeField] private AlchemySO _alchemy;

    [Header(" Result ")]
    [SerializeField] private ItemSlot _resultSlot;
    [SerializeField] private Sprite _resultIcon;


    public void DoAlchemy()
    {
        // Alchemy reduced due to scope. Will be back to normal later on.

        if (_itemSlot2.UIItem != null)
        {
            var herbA = _itemSlot2.UIItem.Item as HerbSO;
            //var herbB = _itemSlot2.UIItem.Item as HerbSO;
            //var herbC = _itemSlot3.UIItem.Item as HerbSO;

            var result = _alchemy.Mix(herbA);
            UIItemFactory.Instance.CreateItem(result, _resultSlot, _resultIcon);

            Destroy(_itemSlot2.UIItem.gameObject);
            //Destroy(_itemSlot2.UIItem.gameObject);
            //Destroy(_itemSlot3.UIItem.gameObject);
        }
    }
}