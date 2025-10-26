using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header(" Main Menu ")]
    public Canvas[] allMenus;
    public Canvas wantedActiveMenu;

    [Header(" Game ")]
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject alchemyMenu;
    public CanvasGroup inventoryMenu;

    public PauseSystem pause;
    public MouseLook mouse;

    void Start()
    {
        Time.timeScale = 1.0f;

        if (allMenus.Length > 0)
        {
            foreach (var menu in allMenus)
            {
                menu.gameObject.SetActive(false);
                if (wantedActiveMenu != null) wantedActiveMenu.gameObject.SetActive(true);
            }
        }
    }

    #region Inventory
    private bool _invSwitch = false;

    void ActivateInventory()
    {
        inventoryMenu.alpha = 1f;

        mouse.LockCamera();
    }
    void DeactivateInventory()
    {
        mouse.UnlockCamera();

        inventoryMenu.alpha = 0f;
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
        alchemyMenu.SetActive(true);

        ActivateInventory();
    }
    public void DeactivateAlchemyMenu()
    {
        alchemyMenu.SetActive(false);

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
        pauseMenu.SetActive(true);

        mouse.LockCamera();
    }

    private void OnEnable()
    {
        InputController.OnPause += ActivatePauseMenu;
        InputController.OnBagToggle += SwitchInventory;
        CraftingManager.OnAlchemyToggle += SwitchAlchemy;
    }
    private void OnDisable()
    {
        InputController.OnPause -= ActivatePauseMenu;
        InputController.OnBagToggle -= ActivateInventory;
        CraftingManager.OnAlchemyToggle -= SwitchAlchemy;
    }
}