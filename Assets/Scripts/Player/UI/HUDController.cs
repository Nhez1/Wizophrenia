using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public void OnInteractableHoverEnter(IInteractable interactable)
    {
        interactMessage.text = interactable.InteractMessage + " (E)";
        interactMessage.gameObject.SetActive(true);
    }

    public void OnInteractableHoverExit(IInteractable interactable)
    {
        interactMessage.gameObject.SetActive(false);
    }

    private void UpdateHealthBar(float hp) => healthBar.value = hp;
    private void UpdateManaBar(float mp) => manaBar.value = mp;

    private void OnEnable()
    {
        PlayerInteraction.OnHoverEnter += OnInteractableHoverEnter;
        PlayerInteraction.OnHoverExit += OnInteractableHoverExit;
        Life.OnHealthChanged += UpdateHealthBar;
        Mana.OnManaChanged += UpdateManaBar;
    }

    private void OnDisable()
    {
        PlayerInteraction.OnHoverEnter -= OnInteractableHoverEnter;
        PlayerInteraction.OnHoverExit -= OnInteractableHoverExit;
        Life.OnHealthChanged -= UpdateHealthBar;
        Mana.OnManaChanged -= UpdateManaBar;
    }
}
