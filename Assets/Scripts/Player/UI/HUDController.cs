using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class HUDController : MonoBehaviour
{
    public Slider healthBar;
    public Slider manaBar;

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

    private void UpdateHealthBar(float hp) => healthBar.value = hp;
    private void UpdateManaBar(float mp) => manaBar.value = mp;

    private void OnEnable()
    {
        Interactable.OnHover += UpdateInteractMessage;
        Life.OnHealthChanged += UpdateHealthBar;
        Mana.OnManaChanged += UpdateManaBar;
    }

    private void OnDisable()
    {
        Interactable.OnHover -= UpdateInteractMessage;
        Life.OnHealthChanged -= UpdateHealthBar;
        Mana.OnManaChanged -= UpdateManaBar;
    }
}
