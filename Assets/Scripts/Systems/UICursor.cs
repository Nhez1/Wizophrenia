using UnityEngine;
using UnityEngine.UI;

public class UICursor : MonoBehaviour
{
    public static UICursor Instance;

    [SerializeField] private Image _customCursor;
    private UIItem _currentItem;

    public UIItem CurrentItem => _currentItem;
    public Image CustomCursor => _customCursor;

    public void PickUp(UIItem item)
    {
        if (_currentItem != null) return;

        _currentItem = item;
        _currentItem.gameObject.transform.SetParent(transform);
    }

    public void ClearHeldItem()
    {
        if (_currentItem != null) _currentItem = null;
        else return;
    }

    private void Awake()
    {
        Instance = this;
        _currentItem = null;

        UpdateCursorPos();
        _customCursor.transform.position = Input.mousePosition;
    }

    void Update() => UpdateCursorPos();

    void UpdateCursorPos() => transform.position = Input.mousePosition;
    public void ActivateCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        if (CustomCursor != null) CustomCursor.gameObject.SetActive(true);
    }
    public void DeactivateCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (CustomCursor != null) CustomCursor.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        MouseLook.OnCameraLock += ActivateCursor;
        MouseLook.OnCameraUnlock += DeactivateCursor;
    }
    private void OnDisable()
    {
        MouseLook.OnCameraLock -= ActivateCursor;
        MouseLook.OnCameraUnlock -= DeactivateCursor;
    }
}
