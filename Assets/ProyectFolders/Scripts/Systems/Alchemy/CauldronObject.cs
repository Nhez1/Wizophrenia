using System;
using UnityEngine;

public class CauldronObject : MonoBehaviour, IInteractable
{
    public static event Action OnAlchemyToggle;
    private readonly string _interactMessage = "Brew";

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

    #region OnHover (not implemented)
    public void OnHoverEnter() => throw new System.NotImplementedException();
    public void OnHoverExit() => throw new System.NotImplementedException();
    public void OnHoverStay() => throw new System.NotImplementedException();
    #endregion
}
