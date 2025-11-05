using UnityEngine;
using UnityEngine.UI;

public class FlameScale : MonoBehaviour
{
    [SerializeField] private Image _flameImage; // assign in inspector
    [SerializeField, Range(0f, 1f)] private float _flameIntensity = 1f;
    [SerializeField] private Vector3 _baseScale = Vector3.one;

    private RectTransform _rect;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        if (_flameImage == null)
            _flameImage = GetComponent<Image>();
    }

    private void Update()
    {
        // Example: make flame smaller when intensity goes down
        _rect.localScale = _baseScale * _flameIntensity;
    }

    // Optional — call this from other scripts
    public void SetFlameSize(float value)
    {
        _flameIntensity = Mathf.Clamp01(value);
    }
}
