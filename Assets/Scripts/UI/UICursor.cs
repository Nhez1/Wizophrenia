using UnityEngine;
using UnityEngine.UI;

public class UICursor : MonoBehaviour
{
    public static UICursor Instance;

    private UIItem _currentItem;

    public UIItem CurrentItem => _currentItem;

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
    }

    void Update() => UpdateCursorPos();

    void UpdateCursorPos() => transform.position = Input.mousePosition;
    public void ActivateCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void DeactivateCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
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
