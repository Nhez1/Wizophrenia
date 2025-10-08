using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public abstract class Interactable : MonoBehaviour
{
    public static event Action<string> OnHover;

    public string interactMessage;
    public UnityEvent onInteraction;

    public bool CanInteract { get; set; }

    public virtual void TryInteract()
    {
        if (CanInteract) Interact();
        else Debug.LogWarning("Can't interact!");
    }

    public virtual void Interact() => onInteraction?.Invoke();

    public void OnHoverUpdate()
    {
        OnHover?.Invoke(interactMessage);
    }
}
// Marker