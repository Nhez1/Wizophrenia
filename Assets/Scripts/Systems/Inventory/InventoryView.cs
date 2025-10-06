using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryView : MonoBehaviour
{
    public static InventoryView Singleton;
    public static InventoryItem carriedItem;
    
    [SerializeField] InventorySlot[] slots;

    [SerializeField] Transform draggablesTransform;

    [Header("Item List")]
    [SerializeField] ItemSO[] items;

    private void Awake()
    {
        Singleton = this;
    }

    private void Update()
    {
        if (carriedItem == null) return;

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