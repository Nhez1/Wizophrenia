using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItem : MonoBehaviour, IPointerClickHandler
{
    private Image _itemIcon;

    [field: SerializeField]
    public ItemSO Item { get; set; }
    public ItemSlot ActiveSlot { get; set; }

    private void Awake()
    {
        _itemIcon = GetComponent<Image>();
    }

    public void Initialize(ItemSO item, ItemSlot parent, Sprite icon = null)
    {
        ActiveSlot = parent;
        ActiveSlot.UIItem = this;
        Item = item;

        if (icon != null) _itemIcon.sprite = icon;
        else _itemIcon.sprite = item.icon;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UIItem item = UICursor.Instance.CurrentItem;

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (item == null)
            {
                UICursor.Instance.PickUp(this);
                if (ActiveSlot.IsHandSlot && ActiveSlot.OnHandSlotClearEvent != null) ActiveSlot.OnHandSlotClearEvent.Raise(this, null);
            }
            else
            {
                item.ActiveSlot.SetItem(item);
                UICursor.Instance.PickUp(this);
            }
        }
    }
}