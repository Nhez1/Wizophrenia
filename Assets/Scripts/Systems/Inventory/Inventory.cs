using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Singleton;
    public static InventoryItem carriedItem;

    [SerializeField] InventorySlot[] slots;

    [SerializeField] Transform draggablesTransform;
    [SerializeField] InventoryItem itemPrefab;

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
        if (carriedItem != null) item.activeSlot.SetItem(carriedItem);

        carriedItem = item;
        carriedItem.canvasGroup.blocksRaycasts = false;
        item.transform.SetParent(draggablesTransform);
    }

    void AddItem(ItemSO item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            //Check if the slot is empty
            if (slots[i].myItem == null)
            {
                Instantiate(itemPrefab, slots[i].transform).Initialize(item, slots[i]);
                break;
            }
        }
    }

    private void OnEnable()
    {
        InternalInventory<ItemSO>.OnItemAdded += AddItem;
    }

    private void OnDisable()
    {
        InternalInventory<ItemSO>.OnItemAdded -= AddItem;
    }
}