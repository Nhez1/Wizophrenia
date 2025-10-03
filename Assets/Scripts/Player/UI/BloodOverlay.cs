using UnityEngine;

public class BloodOverlay : MonoBehaviour
{
    [Header("Overlays de Sangre")]
    public GameObject blood30;
    public GameObject blood50;
    public GameObject blood80;

    private void OnEnable()
    {
        Life.OnHealthChanged += UpdateBloodOverlay;
    }

    private void OnDisable()
    {
        Life.OnHealthChanged -= UpdateBloodOverlay;
    }

    private void UpdateBloodOverlay(float currentHP)
    {
        blood30.SetActive(false);
        blood50.SetActive(false);
        blood80.SetActive(false);

        if (currentHP <= 30) blood30.SetActive(true);
        else if (currentHP <= 50) blood50.SetActive(true);
        else if (currentHP <= 80) blood80.SetActive(true);
    }
}
