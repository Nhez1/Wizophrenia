using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class HUDController : MonoBehaviour
{
    [SerializeField] TMP_Text interactMessage;
    bool interactMessageSwitch = false;

    private void Start()
    {
        if (interactMessage) interactMessage.gameObject.SetActive(interactMessageSwitch);
        else Debug.LogWarning("Interact message reference not applied in the inspector.");
    }

    private void UpdateInteractMessage(string newInteractMessage)
    {
        interactMessageSwitch = !interactMessageSwitch;

        if (interactMessageSwitch) EnableInteractionText(newInteractMessage);
        else DisableInteractionText();
    }

    public void EnableInteractionText(string txt)
    {
        interactMessage.text = txt + " (E)";
        interactMessage.gameObject.SetActive(true);
    }

    public void DisableInteractionText()
    {
        interactMessage.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Interactable.OnHover += UpdateInteractMessage;
    }

    private void OnDisable()
    {
        Interactable.OnHover -= UpdateInteractMessage;
    }
}
