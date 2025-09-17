using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction
{
    public float PlayerReach { get; set; }
    private Interactable currentInteractable;
    private bool isHovering = false;

    public Interactable CurrentInteractable
    {
        get
        {
            if (currentInteractable != null) return currentInteractable;
            else
            {
                Debug.LogWarning("No interactable object in range");
                return null;
            }
        }
        private set { }
    }

    public PlayerInteraction()
    {
    }

    public void HoverUpdate()
    {
        Ray ray = new(Camera.main.transform.position, Camera.main.transform.forward); // Si el jugador mira
        if (Physics.Raycast(ray, out RaycastHit hit, PlayerReach))                   // Un objeto interactuable
        {
            if (hit.collider.TryGetComponent(out Interactable newInteractable))
            {
                if (newInteractable.enabled)
                {
                    if (newInteractable != currentInteractable) isHovering = false;
                    HoverOverNew(newInteractable);
                }
                else HoverLeave(); // Si el nuevo interactuable no está activo
            }
            else HoverLeave(); // Si no es un objeto interactuable
        }
        else HoverLeave(); // Si no hay nada al alcance
    }

    void HoverOverNew(Interactable newInteractable) // Esto se ejecuta cuando se mira a un nuevo objeto.
    {
        currentInteractable = newInteractable;

        if (!isHovering) //This is hardcoded right now, but it will change in the future.
        {
            currentInteractable.OnHoverUpdate();
            isHovering = true;
        }
    }

    void HoverLeave() // Esto se ejecuta cuando se deja de mirar hacia un objeto.
    {
        if (currentInteractable)
        {
            currentInteractable.OnHoverUpdate();
            currentInteractable = null;
            isHovering = false;
        }
    }
}
