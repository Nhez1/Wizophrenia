using System;
using UnityEngine;

public class PlayerInteraction
{
    public static event Action<IInteractable> OnHoverEnter;
    public static event Action<IInteractable> OnHoverExit;

    public float PlayerReach { get; set; }
    private IInteractable currentInteractable;
    public IInteractable CurrentInteractable
    {
        get
        {
            if (currentInteractable != null) return currentInteractable;
            else
            {
                Debug.LogWarning("No interactable object in range.");
                return null;
            }
        }
        private set { }
    }

    public PlayerInteraction() { }

    public void HoverUpdate()
    {
        Ray ray = new(Camera.main.transform.position, Camera.main.transform.forward); // Si el jugador mira
        if (Physics.Raycast(ray, out RaycastHit hit, PlayerReach))                   // Un objeto interactuable dentro de su rango de interacción
        {
            if (hit.collider.TryGetComponent(out IInteractable newInteractable))
            {
                if (newInteractable.IsActive)
                {
                    if (newInteractable != currentInteractable) HoverOverNew(newInteractable);
                }
                else HoverLeave(); // Si el nuevo interactuable no está activo
            }
            else HoverLeave(); // Si no es un objeto interactuable
        }
        else HoverLeave(); // Si no hay nada al alcance
    }

    void HoverOverNew(IInteractable newInteractable) // Esto se ejecuta cuando se mira a un nuevo objeto.
    {
        currentInteractable = newInteractable;
        OnHoverEnter?.Invoke(currentInteractable);
    }

    void HoverLeave() // Esto se ejecuta cuando se deja de mirar hacia un objeto.
    {
        if (currentInteractable != null)
        {
            OnHoverExit?.Invoke(currentInteractable);
            currentInteractable = null;
        }
    }
}
// Marker