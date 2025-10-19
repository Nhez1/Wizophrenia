using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CooldownUI
{
    public Image onImage;
    public Image offImage;
}

public class HUDController : MonoBehaviour
{
    public Slider healthBar;
    public Slider manaBar;

    [SerializeField] private CooldownUI _fireBallCD;
    [SerializeField] private CooldownUI _flameCD;
    [SerializeField] private CooldownUI _reigniteCD;
    private Dictionary<SpellType, CooldownUI> _cdIcons;

    [SerializeField] TMP_Text _interactMessage;
    bool _interactMessageSwitch = false;

    private void Awake()
    {
        _cdIcons = new()
        {
            { SpellType.FireBall, _fireBallCD },
            { SpellType.FlameSpell, _flameCD },
            { SpellType.Reignite, _reigniteCD }
        };
    }

    private void Start()
    {
        if (_interactMessage) _interactMessage.gameObject.SetActive(_interactMessageSwitch);
        else Debug.LogWarning("Interact message reference not applied in the inspector.");
    }

    //-------------------------------------------------------------------------------- Interactables

    public void OnInteractableHoverEnter(IInteractable interactable)
    {
        _interactMessage.text = interactable.InteractMessage + " (E)";
        _interactMessage.gameObject.SetActive(true);
    }

    public void OnInteractableHoverExit(IInteractable interactable)
    {
        _interactMessage.gameObject.SetActive(false);
    }

    //-------------------------------------------------------------------------------- Health & Mana 

    private void UpdateHealthBar(float hp) => healthBar.value = hp;
    private void UpdateManaBar(float mp) => manaBar.value = mp;

    //-------------------------------------------------------------------------------- Cooldowns

    private void CooldownOn(SpellSO spell) 
    {
        _cdIcons[spell.type].onImage.gameObject.SetActive(false);
        _cdIcons[spell.type].offImage.gameObject.SetActive(true);
    }

    private void CooldownOff(SpellSO spell)
    {
        _cdIcons[spell.type].offImage.gameObject.SetActive(false);
        _cdIcons[spell.type].onImage.gameObject.SetActive(true);
    }

    //-------------------------------------------------------------------------------- Events

    private void OnEnable()
    {
        CooldownEffect.OnCooldownStart += CooldownOn;
        CooldownEffect.OnCooldownOver += CooldownOff;
        PlayerInteraction.OnHoverEnter += OnInteractableHoverEnter;
        PlayerInteraction.OnHoverExit += OnInteractableHoverExit;
        Life.OnHealthChanged += UpdateHealthBar;
        Mana.OnManaChanged += UpdateManaBar;
    }

    private void OnDisable()
    {
        CooldownEffect.OnCooldownStart -= CooldownOn;
        CooldownEffect.OnCooldownOver -= CooldownOff;
        PlayerInteraction.OnHoverEnter -= OnInteractableHoverEnter;
        PlayerInteraction.OnHoverExit -= OnInteractableHoverExit;
        Life.OnHealthChanged -= UpdateHealthBar;
        Mana.OnManaChanged -= UpdateManaBar;
    }
}