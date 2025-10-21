using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public UIItem UIItem { get; set; }

    public void SetItem(UIItem item)
    {
        Inventory.carriedItem = null;

        // Reset old slot
        item.ActiveSlot.UIItem = null;

        // Set current slot
        UIItem = item;
        UIItem.ActiveSlot = this;
        UIItem.transform.SetParent(transform);
        UIItem.CanvasGroup.blocksRaycasts = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Inventory.carriedItem == null) return;
            SetItem(Inventory.carriedItem);
        }
    }
}