using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Interactable : MonoBehaviour
{
    public static event Action<string> OnHover;

    public string interactMessage;
    public UnityEvent onInteraction;

    public bool CanInteract { get; set; }

    void Start() => CanInteract = true;

    public void TryInteract()
    {
        if (CanInteract) Interact();
        else Debug.LogWarning("Can't interact!");
    }

    public void Interact() => onInteraction?.Invoke();

    public void OnHoverUpdate()
    {
        OnHover?.Invoke(interactMessage);
    }
}
// Marker