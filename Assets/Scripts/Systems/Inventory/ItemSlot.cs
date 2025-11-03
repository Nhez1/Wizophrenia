using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameEvent _clearHandSlot;
    [SerializeField] private GameEvent _onHandSlotFillEvent;

    public UIItem UIItem { get; set; }
    [field: SerializeField]
    public bool IsHandSlot { get; private set; }

    public void SetItem(UIItem item)
    {
        // Reset old slot
        item.ActiveSlot.UIItem = null;

        // Set current slot
        UIItem = item;
        UIItem.ActiveSlot = this;
        UIItem.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
        UIItem.transform.SetParent(transform);
        if (IsHandSlot && _onHandSlotFillEvent != null) _onHandSlotFillEvent.Raise(this, null);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UIItem item = UICursor.Instance.CurrentItem;

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (item != null)
            {
                if (UIItem == null) SetItem(item);
                else item.ActiveSlot.SetItem(item);

                UICursor.Instance.ClearHeldItem();

                if (item.ActiveSlot.IsHandSlot) _clearHandSlot.Raise(this, null);
            }
        }
    }
}