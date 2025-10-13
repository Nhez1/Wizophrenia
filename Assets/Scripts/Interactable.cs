using System;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    [field: SerializeField]
    public string InteractMessage { get; set; }
    public bool IsActive => gameObject.activeSelf;
    public ItemSO item;
    public Player player;

    public void Interact()
    {
        player.Inventory.AddItem(item);
        gameObject.SetActive(false);
    }

    public void OnHoverEnter()
    {
        throw new NotImplementedException();
    }

    public void OnHoverExit()
    {
        throw new NotImplementedException();
    }

    public void OnHoverStay()
    {
        throw new NotImplementedException();
    }
}
// Marker