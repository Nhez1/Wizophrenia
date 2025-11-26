using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameEvent _clearHandSlot;
    [SerializeField] private GameEvent _fillHandSlot;
    public GameEvent OnHandSlotClearEvent => _clearHandSlot;
    public GameEvent OnHandSlotFillEvent => _fillHandSlot;

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
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var cursor = UICursor.Instance;
        if (cursor.CurrentItem == null) return;
        if (eventData.button != PointerEventData.InputButton.Left) return;

        if (UIItem == null)
        {
            SetItem(cursor.CurrentItem);
            if (IsHandSlot && _fillHandSlot != null) _fillHandSlot.Raise(this, cursor.CurrentItem.Item);
            cursor.ClearCurrentItem();
        }
    }
}