using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    Image itemIcon;
    public CanvasGroup canvasGroup { get; private set; }

    public ItemSO myItem { get; set; }
    public InventorySlot activeSlot { get; set; }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        itemIcon = GetComponent<Image>();
    }

    public void Initialize(ItemSO item, InventorySlot parent)
    {
        activeSlot = parent;
        activeSlot.myItem = this;
        //transform.position = activeSlot.transform.position;
        myItem = item;
        itemIcon.sprite = item.icon;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) Inventory.Singleton.SetCarriedItem(this);
    }
}