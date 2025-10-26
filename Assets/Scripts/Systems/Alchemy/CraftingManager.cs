using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CraftingManager : MonoBehaviour, IInteractable
{
    public static event Action OnAlchemyToggle;

    private readonly string _interactMessage = "Brew";
    //public GameObject alchemyMenu;

    private HerbSO _herb1;
    private HerbSO _herb2;
    private HerbSO _herb3;

    public ItemSlot[] craftingSlots;

    public string InteractMessage => _interactMessage;
    public bool IsActive { get; set; }

    private void Start()
    {
        IsActive = true;
    }

    public void Interact()
    {
        OnAlchemyToggle?.Invoke();
    }

    public void OnHoverEnter()
    {
        throw new System.NotImplementedException();
    }

    public void OnHoverExit()
    {
        throw new System.NotImplementedException();
    }

    public void OnHoverStay()
    {
        throw new System.NotImplementedException();
    }
}
