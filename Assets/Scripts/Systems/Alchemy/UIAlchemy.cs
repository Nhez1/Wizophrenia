using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIAlchemy : MonoBehaviour, IPointerClickHandler
{
    public ItemSlot itemSlot1;
    public ItemSlot itemSlot2;

    public AlchemySO alch;

    void DoAlchemy()
    {
        if (itemSlot1.UIItem != null & itemSlot2.UIItem != null)
        {
        }
    }

    void FilterItems()
    {
        if (itemSlot1.UIItem.Item.type != ItemType.Herb)
        {
            // Impedir que se ponga el item
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
