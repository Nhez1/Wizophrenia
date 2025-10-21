using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItem : MonoBehaviour, IPointerClickHandler
{
    Image itemIcon;
    public CanvasGroup CanvasGroup { get; private set; }

    public ItemSO Item { get; set; }
    public ItemSlot ActiveSlot { get; set; }

    private void Awake()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
        itemIcon = GetComponent<Image>();
    }

    public void Initialize(ItemSO item, ItemSlot parent)
    {
        ActiveSlot = parent;
        ActiveSlot.UIItem = this;
        Item = item;
        itemIcon.sprite = item.icon;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) Inventory.Singleton.SetCarriedItem(this);
    }
}