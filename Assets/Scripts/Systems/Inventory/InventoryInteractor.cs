using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryInteractor : MonoBehaviour
{
    public static InventoryInteractor Singleton;
    public static InventoryItem carriedItem;
    public Player player;
    
    [SerializeField] InventorySlot[] slots;

    [SerializeField] Transform draggablesTransform;

    [Header("Item List")]
    [SerializeField] List<ItemSO> items;

    private void Awake()
    {
        Singleton = this;
    }

    private void Update()
    {
        if (carriedItem == null) return;
        items = player.Inventory.GetAllItems();

        carriedItem.transform.position = Input.mousePosition;
    }
    
    public void SetCarriedItem(InventoryItem item)
    {
        if(carriedItem != null) item.activeSlot.SetItem(carriedItem);

        carriedItem = item;
        carriedItem.canvasGroup.blocksRaycasts = false;
        item.transform.SetParent(draggablesTransform);
    }
}