using UnityEngine;

public class UIItemFactory : MonoBehaviour
{
    public static UIItemFactory Instance { get; private set; }
    [SerializeField] private UIItem _uiItemPrefab;

    private void Awake() => Instance = this;

    public void CreateItem(ItemSO item, ItemSlot slot, Sprite icon = null)
    {
        var uiItem = Instantiate(_uiItemPrefab, slot.transform);
        uiItem.Initialize(item, slot, icon);
    }
}