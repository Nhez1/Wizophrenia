using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header(" Game Menus ")]
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _alchemyMenu;
    [SerializeField] private GameObject _inventoryMenu;
    private CanvasGroup _inventory;

    private PauseSystem _pause;
    private MouseLook _mouse;

    private void Start()
    {
        Time.timeScale = 1.0f;

        _pause = GetComponent<PauseSystem>();
        _mouse = Camera.main.GetComponent<MouseLook>();

        _inventory = _inventoryMenu.GetComponent<CanvasGroup>();
    }

    #region Inventory
    private bool _invSwitch = false;

    void ActivateInventory()
    {
        _inventory.alpha = 1f;

        _mouse.LockCamera();
    }
    void DeactivateInventory()
    {
        _mouse.UnlockCamera();

        _inventory.alpha = 0f;
    }

    void SwitchInventory()
    {
        _invSwitch = !_invSwitch;

        if (_invSwitch) ActivateInventory();
        else DeactivateInventory();
    }
    #endregion

    #region Alchemy
    private bool _alchSwitch = false;

    public void ActivateAlchemyMenu()
    {
        _alchemyMenu.SetActive(true);

        ActivateInventory();
    }
    public void DeactivateAlchemyMenu()
    {
        _alchemyMenu.SetActive(false);

        DeactivateInventory();
    }

    private void SwitchAlchemy()
    {
        _alchSwitch = !_alchSwitch;

        if (_alchSwitch) ActivateAlchemyMenu();
        else DeactivateAlchemyMenu();
    }
    #endregion

    void ActivatePauseMenu()
    {
        _pauseMenu.SetActive(true);

        _mouse.LockCamera();
    }

    private void OnEnable()
    {
        InputController.OnPause += ActivatePauseMenu;
        InputController.OnBagToggle += SwitchInventory;
        CauldronObject.OnAlchemyToggle += SwitchAlchemy;
    }
    private void OnDisable()
    {
        InputController.OnPause -= ActivatePauseMenu;
        InputController.OnBagToggle -= ActivateInventory;
        CauldronObject.OnAlchemyToggle -= SwitchAlchemy;
    }
}