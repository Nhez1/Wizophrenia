using UnityEngine;

public class UICursor : MonoBehaviour
{
    public static UICursor Instance;

    public UIItem CurrentItem => _currentItem;
    private UIItem _currentItem;
    public bool HasItem => _currentItem != null;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _currentItem = null;
    }

    public void PickUp(UIItem item)
    {
        if (item == null) return;

        var prevSlot = item.ActiveSlot;
        var prevItem = _currentItem;

        _currentItem = item;
        _currentItem.transform.SetParent(transform);

        if (prevItem) prevSlot.SetItem(prevItem);
    }

    public void DropCurrentItem()
    {
        if (!HasItem) return;

        _currentItem.ActiveSlot.SetItem(_currentItem);
        _currentItem = null;
    }
    public void ClearCurrentItem() => _currentItem = null;

    private void Update() => transform.position = Input.mousePosition;
    public void ActivateCursor() => Cursor.lockState = CursorLockMode.Confined;
    public void DeactivateCursor() => Cursor.lockState = CursorLockMode.Locked;
    
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
